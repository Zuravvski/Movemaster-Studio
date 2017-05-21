using Driver;
using IDE.Common.Kinect;
using System.Collections.ObjectModel;
using IDE.Common.Models;
// <copyright file="BrowseViewModelTest.cs">Copyright ©  2017</copyright>

using System;
using IDE.Common.ViewModels;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDE.Common.ViewModels.Tests
{
    /// <summary>This class contains parameterized unit tests for BrowseViewModel</summary>
    [TestClass]
    [PexClass(typeof(BrowseViewModel))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class BrowseViewModelTest
    {

        /// <summary>Test stub for .ctor(ProgramEditor, ProgramEditor)</summary>
        [PexMethod]
        public BrowseViewModel ConstructorTest(ProgramEditor commandHistory, ProgramEditor commandInput)
        {
            BrowseViewModel target = new BrowseViewModel(commandHistory, commandInput);
            return target;
            // TODO: add assertions to method BrowseViewModelTest.ConstructorTest(ProgramEditor, ProgramEditor)
        }

        /// <summary>Test stub for get_Appearance()</summary>
        [PexMethod]
        public AppearanceViewModel AppearanceGetTest([PexAssumeUnderTest]BrowseViewModel target)
        {
            AppearanceViewModel result = target.Appearance;
            return result;
            // TODO: add assertions to method BrowseViewModelTest.AppearanceGetTest(BrowseViewModel)
        }

        /// <summary>Test stub for get_AvailableCOMPorts()</summary>
        [PexMethod]
        public ObservableCollection<string> AvailableCOMPortsGetTest([PexAssumeUnderTest]BrowseViewModel target)
        {
            ObservableCollection<string> result = target.AvailableCOMPorts;
            return result;
            // TODO: add assertions to method BrowseViewModelTest.AvailableCOMPortsGetTest(BrowseViewModel)
        }

        /// <summary>Test stub for get_BadgeText()</summary>
        [PexMethod]
        public string BadgeTextGetTest([PexAssumeUnderTest]BrowseViewModel target)
        {
            string result = target.BadgeText;
            return result;
            // TODO: add assertions to method BrowseViewModelTest.BadgeTextGetTest(BrowseViewModel)
        }

        /// <summary>Test stub for get_CommandHistoryText()</summary>
        [PexMethod]
        public string CommandHistoryTextGetTest([PexAssumeUnderTest]BrowseViewModel target)
        {
            string result = target.CommandHistoryText;
            return result;
            // TODO: add assertions to method BrowseViewModelTest.CommandHistoryTextGetTest(BrowseViewModel)
        }

        /// <summary>Test stub for get_CommandInputText()</summary>
        [PexMethod]
        public string CommandInputTextGetTest([PexAssumeUnderTest]BrowseViewModel target)
        {
            string result = target.CommandInputText;
            return result;
            // TODO: add assertions to method BrowseViewModelTest.CommandInputTextGetTest(BrowseViewModel)
        }

        /// <summary>Test stub for get_ConnectionToggleIsChecked()</summary>
        [PexMethod]
        public bool ConnectionToggleIsCheckedGetTest([PexAssumeUnderTest]BrowseViewModel target)
        {
            bool result = target.ConnectionToggleIsChecked;
            return result;
            // TODO: add assertions to method BrowseViewModelTest.ConnectionToggleIsCheckedGetTest(BrowseViewModel)
        }

        /// <summary>Test stub for get_DialogHost()</summary>
        [PexMethod]
        public DialogHost DialogHostGetTest([PexAssumeUnderTest]BrowseViewModel target)
        {
            DialogHost result = target.DialogHost;
            return result;
            // TODO: add assertions to method BrowseViewModelTest.DialogHostGetTest(BrowseViewModel)
        }

        /// <summary>Test stub for get_DialogHostIsOpen()</summary>
        [PexMethod]
        public bool DialogHostIsOpenGetTest([PexAssumeUnderTest]BrowseViewModel target)
        {
            bool result = target.DialogHostIsOpen;
            return result;
            // TODO: add assertions to method BrowseViewModelTest.DialogHostIsOpenGetTest(BrowseViewModel)
        }

        /// <summary>Test stub for get_KinectHandler()</summary>
        [PexMethod]
        public KinectHandler KinectHandlerGetTest([PexAssumeUnderTest]BrowseViewModel target)
        {
            KinectHandler result = target.KinectHandler;
            return result;
            // TODO: add assertions to method BrowseViewModelTest.KinectHandlerGetTest(BrowseViewModel)
        }

        /// <summary>Test stub for get_Manipulator()</summary>
        [PexMethod]
        public E3JManipulator ManipulatorGetTest([PexAssumeUnderTest]BrowseViewModel target)
        {
            E3JManipulator result = target.Manipulator;
            return result;
            // TODO: add assertions to method BrowseViewModelTest.ManipulatorGetTest(BrowseViewModel)
        }

        /// <summary>Test stub for get_RemotePrograms()</summary>
        [PexMethod]
        public ObservableCollection<RemoteProgram> RemoteProgramsGetTest([PexAssumeUnderTest]BrowseViewModel target)
        {
            ObservableCollection<RemoteProgram> result = target.RemotePrograms;
            return result;
            // TODO: add assertions to method BrowseViewModelTest.RemoteProgramsGetTest(BrowseViewModel)
        }

        /// <summary>Test stub for get_SelectedCOMPort()</summary>
        [PexMethod]
        public string SelectedCOMPortGetTest([PexAssumeUnderTest]BrowseViewModel target)
        {
            string result = target.SelectedCOMPort;
            return result;
            // TODO: add assertions to method BrowseViewModelTest.SelectedCOMPortGetTest(BrowseViewModel)
        }

        /// <summary>Test stub for get_SelectedRemoteProgram()</summary>
        [PexMethod]
        public RemoteProgram SelectedRemoteProgramGetTest([PexAssumeUnderTest]BrowseViewModel target)
        {
            RemoteProgram result = target.SelectedRemoteProgram;
            return result;
            // TODO: add assertions to method BrowseViewModelTest.SelectedRemoteProgramGetTest(BrowseViewModel)
        }

        /// <summary>Test stub for get_Settings()</summary>
        [PexMethod]
        public DriverSettings SettingsGetTest([PexAssumeUnderTest]BrowseViewModel target)
        {
            DriverSettings result = target.Settings;
            return result;
            // TODO: add assertions to method BrowseViewModelTest.SettingsGetTest(BrowseViewModel)
        }

        /// <summary>Test stub for set_AvailableCOMPorts(ObservableCollection`1&lt;String&gt;)</summary>
        [PexMethod]
        public void AvailableCOMPortsSetTest([PexAssumeUnderTest]BrowseViewModel target, ObservableCollection<string> value)
        {
            target.AvailableCOMPorts = value;
            // TODO: add assertions to method BrowseViewModelTest.AvailableCOMPortsSetTest(BrowseViewModel, ObservableCollection`1<String>)
        }

        /// <summary>Test stub for set_BadgeText(String)</summary>
        [PexMethod]
        public void BadgeTextSetTest([PexAssumeUnderTest]BrowseViewModel target, string value)
        {
            target.BadgeText = value;
            // TODO: add assertions to method BrowseViewModelTest.BadgeTextSetTest(BrowseViewModel, String)
        }

        /// <summary>Test stub for set_CommandHistoryText(String)</summary>
        [PexMethod]
        public void CommandHistoryTextSetTest([PexAssumeUnderTest]BrowseViewModel target, string value)
        {
            target.CommandHistoryText = value;
            // TODO: add assertions to method BrowseViewModelTest.CommandHistoryTextSetTest(BrowseViewModel, String)
        }

        /// <summary>Test stub for set_CommandInputText(String)</summary>
        [PexMethod]
        public void CommandInputTextSetTest([PexAssumeUnderTest]BrowseViewModel target, string value)
        {
            target.CommandInputText = value;
            // TODO: add assertions to method BrowseViewModelTest.CommandInputTextSetTest(BrowseViewModel, String)
        }

        /// <summary>Test stub for set_ConnectionToggleIsChecked(Boolean)</summary>
        [PexMethod]
        public void ConnectionToggleIsCheckedSetTest([PexAssumeUnderTest]BrowseViewModel target, bool value)
        {
            target.ConnectionToggleIsChecked = value;
            // TODO: add assertions to method BrowseViewModelTest.ConnectionToggleIsCheckedSetTest(BrowseViewModel, Boolean)
        }

        /// <summary>Test stub for set_DialogHost(DialogHost)</summary>
        [PexMethod]
        public void DialogHostSetTest([PexAssumeUnderTest]BrowseViewModel target, DialogHost value)
        {
            target.DialogHost = value;
            // TODO: add assertions to method BrowseViewModelTest.DialogHostSetTest(BrowseViewModel, DialogHost)
        }

        /// <summary>Test stub for set_DialogHostIsOpen(Boolean)</summary>
        [PexMethod]
        public void DialogHostIsOpenSetTest([PexAssumeUnderTest]BrowseViewModel target, bool value)
        {
            target.DialogHostIsOpen = value;
            // TODO: add assertions to method BrowseViewModelTest.DialogHostIsOpenSetTest(BrowseViewModel, Boolean)
        }

        /// <summary>Test stub for set_KinectHandler(KinectHandler)</summary>
        [PexMethod]
        public void KinectHandlerSetTest([PexAssumeUnderTest]BrowseViewModel target, KinectHandler value)
        {
            target.KinectHandler = value;
            // TODO: add assertions to method BrowseViewModelTest.KinectHandlerSetTest(BrowseViewModel, KinectHandler)
        }

        /// <summary>Test stub for set_RemotePrograms(ObservableCollection`1&lt;RemoteProgram&gt;)</summary>
        [PexMethod]
        public void RemoteProgramsSetTest([PexAssumeUnderTest]BrowseViewModel target, ObservableCollection<RemoteProgram> value)
        {
            target.RemotePrograms = value;
            // TODO: add assertions to method BrowseViewModelTest.RemoteProgramsSetTest(BrowseViewModel, ObservableCollection`1<RemoteProgram>)
        }

        /// <summary>Test stub for set_SelectedCOMPort(String)</summary>
        [PexMethod]
        public void SelectedCOMPortSetTest([PexAssumeUnderTest]BrowseViewModel target, string value)
        {
            target.SelectedCOMPort = value;
            // TODO: add assertions to method BrowseViewModelTest.SelectedCOMPortSetTest(BrowseViewModel, String)
        }

        /// <summary>Test stub for set_SelectedRemoteProgram(RemoteProgram)</summary>
        [PexMethod]
        public void SelectedRemoteProgramSetTest([PexAssumeUnderTest]BrowseViewModel target, RemoteProgram value)
        {
            target.SelectedRemoteProgram = value;
            // TODO: add assertions to method BrowseViewModelTest.SelectedRemoteProgramSetTest(BrowseViewModel, RemoteProgram)
        }

        /// <summary>Test stub for set_Settings(DriverSettings)</summary>
        [PexMethod]
        public void SettingsSetTest([PexAssumeUnderTest]BrowseViewModel target, DriverSettings value)
        {
            target.Settings = value;
            // TODO: add assertions to method BrowseViewModelTest.SettingsSetTest(BrowseViewModel, DriverSettings)
        }
    }
}
