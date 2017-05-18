﻿using System;
using System.Globalization;
using System.Windows.Input;
using IDE.Common.Models;
using IDE.Common.ViewModels.Commands;
using Driver;
using System.Linq;
using IDE.Common.Models.Value_Objects;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using Microsoft.Win32;
using IDE.Common.Utilities;
using System.Threading;
using System.Threading.Tasks;
using IDE.Common.Kinect;

namespace IDE.Common.ViewModels
{
    public class BrowseViewModel : ObservableObject
    {

        #region Fields

        private string commandHistoryText;
        private string commandInputText;
        private bool lineWasNotValid;
        private int messageSelectionArrows;
        private ObservableCollection<RemoteProgram> remotePrograms;
        private ObservableCollection<string> availableCOMPorts;
        private RemoteProgram selectedRemoteProgram;
        private E3JManipulator manipulator;
        private DriverSettings settings;
        private ProgramService programService;
        private readonly ProgramEditor commandHistory, commandInput;

        private string selectedCOMPort;
        private string badgeText;
        private DialogHost dialogHost;
        private bool dialogHostIsOpen;
        private bool connectionToggleIsChecked;
        private KinectHandler kinectHandler;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of BrowseViewModel class.
        /// </summary>
        public BrowseViewModel(ProgramEditor commandHistory, ProgramEditor commandInput)
        {
            DeclareCommands();

            this.commandInput = commandInput;
            commandInput.PreviewKeyDown += commandInput_PreviewKeyDown;
            commandInput.TextChanged += commandInput_TextChanged;

            this.commandHistory = commandHistory;
            commandHistory.PreviewMouseWheel += commandHistory_PreviewMouseWheel;

            MessageList = new MessageList();
            Settings = DriverSettings.CreateDefaultSettings();
            KinectHandler = new KinectHandler();
            
            //this should be removed later on
            manipulator = new E3JManipulator(DriverSettings.CreateDefaultSettings());
        }

        #endregion

        #region Properties

        public KinectHandler KinectHandler
        {
            get { return kinectHandler; }
            set
            {
                kinectHandler = value;
                NotifyPropertyChanged("KinectHandler");
            }
        }

        public bool ConnectionToggleIsChecked
        {
            get { return connectionToggleIsChecked; }
            set
            {
                connectionToggleIsChecked = value;
                if (connectionToggleIsChecked)
                    BadgeText = string.Empty;
                NotifyPropertyChanged("ConnectionToggleIsChecked");
            }
        }

        public string BadgeText
        {
            get { return badgeText; }
            set
            {
                badgeText = value;
                NotifyPropertyChanged("BadgeText");
            }
        }

        public bool DialogHostIsOpen
        {
            get { return dialogHostIsOpen; }
            set
            {
                dialogHostIsOpen = value;
                if (!DialogHostIsOpen)
                {
                    DialogHost.CancellationTokenSource.Cancel();
                }
                NotifyPropertyChanged("DialogHostIsOpen");
            }
        }

        public DialogHost DialogHost
        {
            get { return dialogHost; }
            set
            {
                dialogHost = value;
                NotifyPropertyChanged("DialogHost");
            }
        }

        public DriverSettings Settings
        {
            get { return settings; }
            set
            {
                settings = value;
                NotifyPropertyChanged("Settings");
            }
        }

        public string SelectedCOMPort
        {
            get { return selectedCOMPort; }
            set
            {
                selectedCOMPort = value;
                NotifyPropertyChanged("SelectedCOMPort");
            }
        }

        public ObservableCollection<string> AvailableCOMPorts
        {
            get { return availableCOMPorts; }
            set
            {
                availableCOMPorts = value;
                NotifyPropertyChanged("AvailableCOMPorts");
            }
        }

        public string CommandInputText
        {
            get
            {
                return commandInputText;
            }
            set
            {
                commandInputText = value;
                NotifyPropertyChanged("CommandInputText");
            }
        }

        public string CommandHistoryText
        {
            get
            {
                return commandHistoryText;
            }
            set
            {
                commandHistoryText = value;
                NotifyPropertyChanged("CommandHistoryText");
            }
        }

        public AppearanceViewModel Appearance => AppearanceViewModel.Instance;

        public RemoteProgram SelectedRemoteProgram
        {
            get
            {
                return selectedRemoteProgram;
            }
            set
            {
                selectedRemoteProgram = value;
                NotifyPropertyChanged("SelectedRemoteProgram");
            }
        }

        public ObservableCollection<RemoteProgram> RemotePrograms
        {
            get
            {
                return remotePrograms;
            }
            set
            {
                remotePrograms = value;
                NotifyPropertyChanged("RemotePrograms");
            }
        }

        public E3JManipulator Manipulator
        {
            get
            {
                return manipulator;
            }
            private set
            {
                manipulator = value;
                programService = new ProgramService(manipulator);
                programService.StepUpdate += ProgramService_StepUpdate;
                NotifyPropertyChanged("Manipulator");
            }
        }

        /// <summary>
        /// List storing sent commands,
        /// </summary>
        public MessageList MessageList { get; }

        #endregion

        #region Actions

        private void Port_ConnectionStatusChanged(object sender, ConnectionStatusChangedArgs e)
        {
            if (e.OldStatus == true && e.NewStatus == false)
            {
                Manipulator.Port.DataReceived -= Port_DataReceived;
                ConnectionToggleIsChecked = false;
                SelectedCOMPort = null;
            }
        }

        private async void ProgramService_StepUpdate(object sender, NotificationEventArgs e)
        {
            int progress = (int)(e.CurrentStep / (float)e.NumberOfSteps * 100);
            CreateDialogHost(false, e.ActionName, new CancellationTokenSource(), progress);

            if (e.CurrentStep == e.NumberOfSteps)
            {
                await Task.Delay(2000);
                Refresh(null);
            }
        }

        private void CreateDialogHost(bool isIndeterminate, string currentAction, CancellationTokenSource cancellationToken, int currentProgress = 0)
        {
            var message = "";
            var progress = "";

            if (isIndeterminate)
            {
                message = "Just a moment...";   //default indeterminate dialog 
            }
            else
            {
                if (currentProgress < 30)
                    message = "Hold on. Looks like it might take a while.";
                else if (currentProgress < 60)
                    message = "How about you get yourself some coffee?";
                else if (currentProgress < 90)
                    message = "Well, worst part is over, right?";
                else
                    message = "Get ready. We are almost done.";

                progress = currentProgress.ToString() + "%";
            }

            DialogHost = new DialogHost()
            {
                CurrentAction = currentAction,
                CurrentProgress = progress,
                Message = message,
                CancellationTokenSource = cancellationToken
            };
        }

        /// <summary>
        /// Occurs when there is any text change in Command Input editor.
        /// </summary>
        private async void commandInput_TextChanged(object sender, EventArgs e)
        {
            if (lineWasNotValid)
            {
                var isLineValid = await commandInput.ValidateLine(1);

                if (isLineValid)
                {
                    lineWasNotValid = false;
                }
            }

            if (CommandInputText.Equals(string.Empty))
                messageSelectionArrows = 0;
        }


        /// <summary>
        /// Occurs after user triggers upload event.
        /// </summary>
        /// <param name="obj"></param>
        private async void Refresh(object obj)
        {
            DialogHostIsOpen = true;
            CreateDialogHost(true, "Refreshing program list", new CancellationTokenSource());
            RemotePrograms = null;
            RemotePrograms = new ObservableCollection<RemoteProgram>(new List<RemoteProgram>(await programService.ReadProgramInfo()));
            DialogHostIsOpen = false;
        }

        /// <summary>
        /// Occurs after user triggers upload event.
        /// </summary>
        private async void Download(object obj)
        {  
            var dialog = new SaveFileDialog()
            {
                Filter = "Text file (.txt)|*.txt",
                FileName = SelectedRemoteProgram.Name
            };

            if (dialog.ShowDialog().GetValueOrDefault(false))
            {
                DialogHostIsOpen = true;
                CreateDialogHost(true, $"Downloading {SelectedRemoteProgram.Name}...", new CancellationTokenSource());
                var program = await programService.DownloadProgram(SelectedRemoteProgram);
                var programWithoutLineNumbers = ProgramContentConverter.ToPC(program.Content);
                program.Content = programWithoutLineNumbers;
                File.WriteAllText(dialog.FileName, program.Content);
            }

            DialogHostIsOpen = false;
        }

        /// <summary>
        /// Deletes specified program and position data.
        /// </summary>
        /// <param name="obj"></param>
        private async void Delete(object obj)
        {
            DialogHostIsOpen = true;
            programService.DeleteProgram(SelectedRemoteProgram.Name);
            await Task.Delay(2000);
            Refresh(null);
        }

        /// <summary>
        /// Occurs after user triggers upload event.
        /// </summary>
        private async void Upload(object obj)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Text files (.txt)|*.txt",
                CheckFileExists = true
            };
            

            if (dialog.ShowDialog().GetValueOrDefault(false))
            {
                var path = dialog.FileName;

                if (string.IsNullOrWhiteSpace(path) || Uri.IsWellFormedUriString(path, UriKind.RelativeOrAbsolute))
                    return;

                var name = Path.GetFileNameWithoutExtension(path);
                var content = File.ReadAllText(dialog.FileName);
                var program = new Program(name) {Content = content};

                DialogHostIsOpen = true;
                CreateDialogHost(false, $"Uploading program", new CancellationTokenSource());
                var contentWithLineNumbers = ProgramContentConverter.ToManipulator(program.Content);
                program.Content = contentWithLineNumbers;

                var cancellationToken = new CancellationTokenSource();
                await programService.UploadProgram(program, cancellationToken.Token);
            }
        }

        /// <summary>
        /// Occurs after user triggers send event.
        /// </summary>
        private async void Send(object obj = null)
        {
            if (Manipulator.Connected && !string.IsNullOrWhiteSpace(commandInputText))
            {
                if (commandInput.DoSyntaxCheck != true) //if user dont want to check syntax just send it right away
                {
                    //syntaxCheckVisualizer.Visualize(true, line);
                    MessageList.AddMessage(new Message(DateTime.Now, commandInput.Text, Message.Type.Send));
                    CommandHistoryText += MessageList.Messages[MessageList.Messages.Count - 1].DisplayMessage();
                    manipulator.SendCustom(MessageList.Messages[MessageList.Messages.Count - 1].MyMessage); //send
                    commandHistory.ScrollToEnd();
                    CommandInputText = string.Empty;
                    messageSelectionArrows = 0;
                }
                else //if user wants to check syntax
                {
                    var isLineValid = await commandInput.ValidateLine(1);

                    if (isLineValid)    //if line is valid, send it
                    {
                        MessageList.AddMessage(new Message(DateTime.Now, commandInput.Text, Message.Type.Send));
                        CommandHistoryText += MessageList.Messages[MessageList.Messages.Count - 1].DisplayMessage();
                        manipulator.SendCustom(MessageList.Messages[MessageList.Messages.Count - 1].MyMessage); //send
                        commandHistory.ScrollToEnd();
                        CommandInputText = string.Empty;
                        messageSelectionArrows = 0;
                    }
                    else //if line is not valid colorize line and don't send
                    {
                        lineWasNotValid = true;
                    }
                }
            }
        }

        private void Stop(object obj)
        {
            programService.StopProgram();
        }

        private void Run(object obj)
        {
            programService.RunProgram(SelectedRemoteProgram);
        }

        /// <summary>
        /// Occurs after user triggers font reduce event.
        /// </summary>
        private void FontReduce(object obj = null)
        {
            if (commandHistory.FontSize > 3)
            {
                commandHistory.FontReduce();
            }
        }

        /// <summary>
        /// Occurs after user triggers font enlarge event.
        /// </summary>
        private void FontEnlarge(object obj = null)
        {
            if (commandHistory.FontSize < 20)
            {
                commandHistory.FontEnlarge();
            }
        }

        /// <summary>
        /// Exports current content of Command History to a file.
        /// </summary>
        /// <param name="obj"></param>
        private void ExportHistory(object obj)
        {
            commandHistory.ExportContent(DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(':', '-'), "txt");
        }

        /// <summary>
        /// Clears current content of Command History.
        /// </summary>
        /// <param name="obj"></param>
        private void ClearHistory(object obj)
        {
            CommandHistoryText = string.Empty;
        }

        /// <summary>
        /// Sets current font.
        /// </summary>
        /// <param name="obj">Current font.</param>
        private void ChangeFont(object obj)
        {
            var font = obj as string;

            commandInput.ChangeFont(font);
            commandHistory.ChangeFont(font);
        }
        
        /// <summary>
        /// Occurs when there is any key down while having focus on Command Input editor.
        /// </summary>
        private void commandInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !commandInput.IsIntellisenseShowing)
                Send();


            if (!commandInput.IsIntellisenseShowing)  //if theres no completion window use arrows to show previous messages
            {
                var sentMessages = MessageList.Messages.Where(i => i.MyType == Message.Type.Send).ToList();

                if (e.Key == Key.Up)
                {
                    if (messageSelectionArrows < sentMessages.Count)
                    {
                        commandInput.Text = sentMessages[sentMessages.Count - ++messageSelectionArrows].MyMessage;
                        //commandInput.Text = MessageList.Messages[MessageList.Messages.Count - ++messageSelectionArrows].MyMessage;
                        commandInput.TextArea.Caret.Offset = commandInput.Text.Length;  //bring carret to end of text
                    }
                }
                else if (e.Key == Key.Down)
                {
                    if (messageSelectionArrows > 1)
                    {
                        commandInput.Text = sentMessages[sentMessages.Count - --messageSelectionArrows].MyMessage;
                        //commandInput.Text = MessageList.Messages[MessageList.Messages.Count - --messageSelectionArrows].MyMessage;
                        commandInput.TextArea.Caret.Offset = commandInput.Text.Length;  //bring carret to end of text
                    }
                    else if (messageSelectionArrows > 0)
                    {
                        --messageSelectionArrows;
                        commandInput.Text = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Occurs when there is any mouse scroll press/movement while having focus on Command Input editor.
        /// </summary>
        private void commandHistory_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (!handle)
                return;

            if (e.Delta > 0)    //scrolls away from user
                FontEnlarge();
            else if (e.Delta < 0)
                FontReduce();   //scrolls toward user
        }

        #endregion

        #region Commands

        public ICommand RefreshClickCommand { get; private set; }
        public ICommand DownloadClickCommand { get; private set; }
        public ICommand UploadClickCommand { get; private set; }
        public ICommand SendClickCommand { get; private set; }
        public ICommand RunClickCommand { get; private set; }
        public ICommand DeleteClickCommand { get; private set; }
        public ICommand ClearHistoryCommand { get; private set; }
        public ICommand ExportHistoryCommand { get; private set; }
        public ICommand ChangeFontCommand { get; private set; }
        public ICommand ConnectionCommand { get; private set; }
        public ICommand RefreshCOMPortsCommand { get; private set; }

        private void DeclareCommands()
        {
            RefreshClickCommand = new RelayCommand(Refresh, IsConnectionEstablished);
            DownloadClickCommand = new RelayCommand(Download);
            UploadClickCommand = new RelayCommand(Upload, IsConnectionEstablished);
            SendClickCommand = new RelayCommand(Send, IscommandInputNotEmpty);
            RunClickCommand = new RelayCommand(Run, IsConnectionEstablished);
            DeleteClickCommand = new RelayCommand(Delete, IsConnectionEstablished);
            ClearHistoryCommand = new RelayCommand(ClearHistory, IscommandHistoryNotEmpty);
            ExportHistoryCommand = new RelayCommand(ExportHistory, IscommandHistoryNotEmpty);
            ChangeFontCommand = new RelayCommand(ChangeFont, CanChangeFont);
            ConnectionCommand = new RelayCommand(Connection);
            RefreshCOMPortsCommand = new RelayCommand(RefreshCOMPorts);
        }

        private void RefreshCOMPorts(object obj)
        {
            AvailableCOMPorts = new ObservableCollection<string>(SerialPort.GetPortNames());
        }

        private async void Connection(object obj)
        {
            if (null != obj)
            {
                var state = (bool)obj;
                if (!state)
                {
                    Manipulator?.Disconnect();
                }
                else
                {
                    if (string.IsNullOrEmpty(SelectedCOMPort))
                    {
                        BadgeText = "!";
                        ConnectionToggleIsChecked = false;
                        return;
                    }

                    ConnectionToggleIsChecked = true;
                    Manipulator = new E3JManipulator(Settings);
                    Manipulator.Port.ConnectionStatusChanged += Port_ConnectionStatusChanged;
                    Manipulator.Connect(SelectedCOMPort);
                    Manipulator.Port.DataReceived += Port_DataReceived;
                }
            }
        }

        private void Port_DataReceived(string data)
        {
            data = data.Replace("\r", string.Empty);
            MessageList.AddMessage(new Message(DateTime.Now, data, Message.Type.Received));

            var receivedMessages = MessageList.Messages.Where(i => i.MyType == Message.Type.Received).ToList();
            CommandHistoryText += receivedMessages[receivedMessages.Count - 1].DisplayMessage();
            commandHistory.Dispatcher.Invoke(() => commandHistory.ScrollToEnd());
        }

        private bool CanChangeFont(object obj)
        {
            var text = commandHistory.FontFamily.ToString();
            return !text.Equals(obj as string);
        }
        
        /// <summary>
        /// Return a value based upon wheter Command History is empty or not.
        /// </summary>
        private bool IscommandHistoryNotEmpty(object obj)
        {
            return !string.IsNullOrWhiteSpace(CommandHistoryText);
        }

        /// <summary>
        /// Return a value based upon wheter a connection between computer and RV-E3J manipulator was established or not.
        /// </summary>
        private bool IsConnectionEstablished(object obj)
        {
            var isConnected = Manipulator.Connected;
            if (!isConnected)
                ConnectionToggleIsChecked = false;

            return isConnected;
        }


        /// <summary>
        /// Returns a value based upon wheter a Command Input is empty or not.
        /// </summary>
        private bool IscommandInputNotEmpty(object obj)
        {
            return !string.IsNullOrWhiteSpace(CommandInputText);
        }

        #endregion

    }
}
