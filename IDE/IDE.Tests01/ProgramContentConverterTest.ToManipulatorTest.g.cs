using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// <copyright file="ProgramContentConverterTest.ToManipulatorTest.g.cs">Copyright ©  2017</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace IDE.Common.Utilities.Tests
{
    public partial class ProgramContentConverterTest
    {

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void ToManipulatorTestThrowsNullReferenceException239()
{
    string s;
    s = this.ToManipulatorTest((string)null);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest950()
{
    string s;
    s = this.ToManipulatorTest("");
    Assert.AreEqual<string>("1 ", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest284()
{
    string s;
    s = this.ToManipulatorTest("\0");
    Assert.AreEqual<string>("1 \0", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest720()
{
    string s;
    s = this.ToManipulatorTest("\r");
    Assert.AreEqual<string>("1 \r", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest859()
{
    string s;
    s = this.ToManipulatorTest("\r\0");
    Assert.AreEqual<string>("1 \r\0", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest935()
{
    string s;
    s = this.ToManipulatorTest("\r\v");
    Assert.AreEqual<string>("1 \r\v", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest20()
{
    string s;
    s = this.ToManipulatorTest("\r\n");
    Assert.AreEqual<string>("1 \r\n6 ", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest32()
{
    string s;
    s = this.ToManipulatorTest("\r\0\r");
    Assert.AreEqual<string>("1 \r\0\r", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest653()
{
    string s;
    s = this.ToManipulatorTest("\r\n\0");
    Assert.AreEqual<string>("1 \r\n6 \0", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest779()
{
    string s;
    s = this.ToManipulatorTest("\r\n\r\0");
    Assert.AreEqual<string>("1 \r\n6 \r\0", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest474()
{
    string s;
    s = this.ToManipulatorTest("\r\0\r\v");
    Assert.AreEqual<string>("1 \r\0\r\v", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest441()
{
    string s;
    s = this.ToManipulatorTest("\r\n\r\n");
    Assert.AreEqual<string>("1 \r\n6 \r\n11 ", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest626()
{
    string s;
    s = this.ToManipulatorTest("\n");
    Assert.AreEqual<string>("1 \r\n6 ", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest835()
{
    string s;
    s = this.ToManipulatorTest("\n\0\n");
    Assert.AreEqual<string>("1 \r\n6 \0\r\n11 ", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest154()
{
    string s;
    s = this.ToManipulatorTest("\n\r\0\n");
    Assert.AreEqual<string>("1 \r\n6 \r\0\r\n11 ", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest978()
{
    string s;
    s = this.ToManipulatorTest("\r\v\r\v");
    Assert.AreEqual<string>("1 \r\v\r\v", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest539()
{
    string s;
    s = this.ToManipulatorTest("\r\0\r\0");
    Assert.AreEqual<string>("1 \r\0\r\0", s);
}

[TestMethod]
[PexGeneratedBy(typeof(ProgramContentConverterTest))]
public void ToManipulatorTest525()
{
    string s;
    s = this.ToManipulatorTest("\n\n\n\r\0\n\n");
    Assert.AreEqual<string>("1 \r\n6 \r\n11 \r\n16 \r\0\r\n21 \r\n26 ", s);
}
    }
}
