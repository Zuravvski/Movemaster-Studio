// <copyright file="MissingFileManagerTest.cs">Copyright ©  2017</copyright>
using System;
using IDE.Common.Utilities;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDE.Common.Utilities.Tests
{
    /// <summary>This class contains parameterized unit tests for MissingFileManager</summary>
    [PexClass(typeof(MissingFileManager))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class MissingFileManagerTest
    {
        /// <summary>Test stub for CheckForRequiredFiles()</summary>
        [PexMethod]
        public void CheckForRequiredFilesTest()
        {
            MissingFileManager.CheckForRequiredFiles();
            // TODO: add assertions to method MissingFileManagerTest.CheckForRequiredFilesTest()
        }

        /// <summary>Test stub for CreateCommandsFile()</summary>
        [PexMethod]
        public void CreateCommandsFileTest()
        {
            MissingFileManager.CreateCommandsFile();
            // TODO: add assertions to method MissingFileManagerTest.CreateCommandsFileTest()
        }

        /// <summary>Test stub for CreateHighlightingDefinitionFile()</summary>
        [PexMethod]
        public void CreateHighlightingDefinitionFileTest()
        {
            MissingFileManager.CreateHighlightingDefinitionFile();
            // TODO: add assertions to method MissingFileManagerTest.CreateHighlightingDefinitionFileTest()
        }

        /// <summary>Test stub for CreateMissingFiles()</summary>
        [PexMethod]
        public void CreateMissingFilesTest()
        {
            MissingFileManager.CreateMissingFiles();
            // TODO: add assertions to method MissingFileManagerTest.CreateMissingFilesTest()
        }

        /// <summary>Test stub for CreateSessionFile()</summary>
        [PexMethod]
        public void CreateSessionFileTest()
        {
            MissingFileManager.CreateSessionFile();
            // TODO: add assertions to method MissingFileManagerTest.CreateSessionFileTest()
        }
    }
}
