// <copyright file="ProgramContentConverterTest.cs">Copyright ©  2017</copyright>
using System;
using IDE.Common.Utilities;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IDE.Common.Utilities.Tests
{
    /// <summary>This class contains parameterized unit tests for ProgramContentConverter</summary>
    [PexClass(typeof(ProgramContentConverter))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class ProgramContentConverterTest
    {
        /// <summary>Test stub for ToManipulator(String)</summary>
        [PexMethod]
        public string ToManipulatorTest(string content)
        {
            string result = ProgramContentConverter.ToManipulator(content);
            return result;
            // TODO: add assertions to method ProgramContentConverterTest.ToManipulatorTest(String)
        }

        /// <summary>Test stub for ToManipulator(String[])</summary>
        [PexMethod]
        public string[] ToManipulatorTest01(string[] content)
        {
            string[] result = ProgramContentConverter.ToManipulator(content);
            return result;
            // TODO: add assertions to method ProgramContentConverterTest.ToManipulatorTest01(String[])
        }

        /// <summary>Test stub for ToPC(String)</summary>
        [PexMethod]
        public string ToPCTest(string content)
        {
            string result = ProgramContentConverter.ToPC(content);
            return result;
            // TODO: add assertions to method ProgramContentConverterTest.ToPCTest(String)
        }

        /// <summary>Test stub for ToPC(String[])</summary>
        [PexMethod]
        public string[] ToPCTest01(string[] content)
        {
            string[] result = ProgramContentConverter.ToPC(content);
            return result;
            // TODO: add assertions to method ProgramContentConverterTest.ToPCTest01(String[])
        }
    }
}
