using System.IO;
using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IDE.Common.ViewModels;
// <copyright file="EditorViewModelTest.ConstructorTest.g.cs">Copyright ©  2017</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace IDE.Common.ViewModels.Tests
{
    public partial class EditorViewModelTest
    {

[TestMethod]
[PexGeneratedBy(typeof(EditorViewModelTest))]
[PexRaisedException(typeof(FileNotFoundException))]
public void ConstructorTestThrowsFileNotFoundException754()
{
    EditorViewModel editorViewModel;
    editorViewModel = this.ConstructorTest();
}
    }
}
