using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IDE.Common.Models;
using IDE.Common.ViewModels;
// <copyright file="BrowseViewModelTest.ConstructorTest.g.cs">Copyright ©  2017</copyright>
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
    public partial class BrowseViewModelTest
    {

[TestMethod]
[PexGeneratedBy(typeof(BrowseViewModelTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void ConstructorTestThrowsNullReferenceException630()
{
    BrowseViewModel browseViewModel;
    browseViewModel = this.ConstructorTest((ProgramEditor)null, (ProgramEditor)null);
}
    }
}
