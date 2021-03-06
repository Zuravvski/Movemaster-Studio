﻿using System;
using System.Globalization;
using System.Windows.Input;
using IDE.Common.Models;
using IDE.Common.ViewModels.Commands;
using Driver;
using IDE.Common.Models.Value_Objects;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;

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
        private readonly ProgramService programService;
        private readonly ProgramEditor commandHistory, commandInput;

        private string selectedCOMPort;
        private DialogHost dialogHost;
        private bool dialogHostIsOpen;

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
            
            //this should be removed later on
            manipulator = new E3JManipulator(DriverSettings.CreateDefaultSettings());
            programService = new ProgramService(manipulator);

            RemotePrograms = new ObservableCollection<RemoteProgram>(new List<RemoteProgram>())
            {
                new RemoteProgram("Pierwszy", 2567, "10-03-15 11:12:56"),
                new RemoteProgram("Wtorek", 1200, "08-06-17 09:34:43"),
                new RemoteProgram("Asd", 45, "17-11-24 04:32:23"),
                new RemoteProgram("qwerty", 52789, "29-09-32 18:14:32")
            };
        }

        #endregion

        #region Properties

        public bool DialogHostIsOpen
        {
            get { return dialogHostIsOpen; }
            set
            {
                dialogHostIsOpen = value;
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
                NotifyPropertyChanged("Manipulator");
            }
        }

        /// <summary>
        /// List storing sent commands,
        /// </summary>
        public MessageList MessageList { get; }

        #endregion

        #region Actions
        
        private void CreateDialogHost(bool isIndeterminate, string currentAction, string currentProgress = "", string message = "")
        {
            if (isIndeterminate && message.Equals(string.Empty))
                message = "Just a moment...";   //default indeterminate dialog message

            DialogHost = new DialogHost()
            {
                CurrentAction = currentAction,
                CurrentProgress = currentProgress,
                Message = message
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
            RemotePrograms = new ObservableCollection<RemoteProgram>(new List<RemoteProgram>(await programService.ReadProgramInfo()));
        }

        /// <summary>
        /// Occurs after user triggers upload event.
        /// </summary>
        private async void Download(object obj)
        {
            await programService.DownloadProgram(SelectedRemoteProgram);
        }

        /// <summary>
        /// Occurs after user triggers upload event.
        /// </summary>
        private void Upload(object obj)
        {
            programService.UploadProgram();
        }

        /// <summary>
        /// Occurs after user triggers send event.
        /// </summary>
        private async void Send(object obj = null)
        {
            if (!string.IsNullOrWhiteSpace(commandInputText))
            {
                if (commandInput.DoSyntaxCheck != true) //if user dont want to check syntax just send it right away
                {
                    //syntaxCheckVisualizer.Visualize(true, line);
                    MessageList.AddMessage(new Message(DateTime.Now, commandInput.Text));
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
                        MessageList.AddMessage(new Message(DateTime.Now, commandInput.Text));
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
                if (e.Key == Key.Up)
                {
                    if (messageSelectionArrows < MessageList.Messages.Count)
                    {
                        commandInput.Text = MessageList.Messages[MessageList.Messages.Count - ++messageSelectionArrows].MyMessage;
                        commandInput.TextArea.Caret.Offset = commandInput.Text.Length;  //bring carret to end of text
                    }
                }
                else if (e.Key == Key.Down)
                {
                    if (messageSelectionArrows > 1)
                    {
                        commandInput.Text = MessageList.Messages[MessageList.Messages.Count - --messageSelectionArrows].MyMessage;
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
        public ICommand StopClickCommand { get; private set; }
        public ICommand ClearHistoryCommand { get; private set; }
        public ICommand ExportHistoryCommand { get; private set; }
        public ICommand ChangeFontCommand { get; private set; }
        public ICommand ConnectionCommand { get; private set; }
        public ICommand RefreshCOMPortsCommand { get; private set; }

        private void DeclareCommands()
        {
            RefreshClickCommand = new RelayCommand(Refresh, IsConnectionEstablished);
            DownloadClickCommand = new RelayCommand(Download, IsItemSelected);
            UploadClickCommand = new RelayCommand(Upload, IsConnectionEstablished);
            SendClickCommand = new RelayCommand(Send, IscommandInputNotEmpty);
            RunClickCommand = new RelayCommand(Run, IsConnectionEstablished);
            StopClickCommand = new RelayCommand(Stop, IsConnectionEstablished);
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
                    Manipulator = new E3JManipulator(Settings);
                    Manipulator.Connect(SelectedCOMPort);
                }
            }
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
            //TODO
            return true;
        }

        /// <summary>
        /// Returns a value based upon wheter a item is selected in remote program list or not.
        /// </summary>
        private bool IsItemSelected(object obj)
        {
            //if (SelecteRemotedProgram != null)
            //    return true;
            //else
            //    return false;
            return true;
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
