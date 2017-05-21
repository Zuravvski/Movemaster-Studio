using System.Collections.Generic;
using IDE.Common.Models;
using System.Collections.ObjectModel;
using Driver;
// <copyright file="SessionTest.cs">Copyright ©  2017</copyright>

using System;
using IDE.Common.Utilities;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDE.Common.Utilities.Tests
{
    /// <summary>This class contains parameterized unit tests for Session</summary>
    [TestClass]
    [PexClass(typeof(Session))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class SessionTest
    {

        /// <summary>Test stub for Initialize()</summary>
        [PexMethod]
        public void InitializeTest([PexAssumeUnderTest]Session target)
        {
            target.Initialize();
            // TODO: add assertions to method SessionTest.InitializeTest(Session)
        }

        /// <summary>Test stub for LoadPrograms()</summary>
        [PexMethod]
        public ObservableCollection<Program> LoadProgramsTest([PexAssumeUnderTest]Session target)
        {
            ObservableCollection<Program> result = target.LoadPrograms();
            return result;
            // TODO: add assertions to method SessionTest.LoadProgramsTest(Session)
        }

        /// <summary>Test stub for SavePrograms(IEnumerable`1&lt;TabItem&gt;)</summary>
        [PexMethod]
        public void SaveProgramsTest([PexAssumeUnderTest]Session target, IEnumerable<TabItem> tabItems)
        {
            target.SavePrograms(tabItems);
            // TODO: add assertions to method SessionTest.SaveProgramsTest(Session, IEnumerable`1<TabItem>)
        }

        /// <summary>Test stub for SubmitCommands(String)</summary>
        [PexMethod]
        public void SubmitCommandsTest([PexAssumeUnderTest]Session target, string path)
        {
            target.SubmitCommands(path);
            // TODO: add assertions to method SessionTest.SubmitCommandsTest(Session, String)
        }

        /// <summary>Test stub for SubmitHighlighting(String)</summary>
        [PexMethod]
        public void SubmitHighlightingTest([PexAssumeUnderTest]Session target, string path)
        {
            target.SubmitHighlighting(path);
            // TODO: add assertions to method SessionTest.SubmitHighlightingTest(Session, String)
        }

        /// <summary>Test stub for get_Instance()</summary>
        [PexMethod]
        public Session InstanceGetTest()
        {
            Session result = Session.Instance;
            return result;
            // TODO: add assertions to method SessionTest.InstanceGetTest()
        }
    }
}
