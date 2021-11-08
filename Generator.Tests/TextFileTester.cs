// <copyright file="TextFileTester.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Generator.Tests
{
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Paper_csharp.Modules.File_parser;

  [TestClass]
  public class TextFileTester
  {
    [TestMethod]
    public void ParseWithTitle()
    {
      var result = TextFile.Parse("Title\n\n\nHello World");
      var expect = new ParseResult("Title", "<h1>Title</h1><p>Hello World</p>");
      TestUtils.AreEqualByJson(expect, result);
    }

    [TestMethod]
    public void ParseWithoutTitle()
    {
      var result = TextFile.Parse("Hello World");
      var expect = new ParseResult(string.Empty, "<p>Hello World</p>");
      TestUtils.AreEqualByJson(expect, result);
    }

    [TestMethod]
    public void ParseWithInvalidTitle()
    {
      var result = TextFile.Parse("Title\n\nHello World");
      var expect = new ParseResult(string.Empty, "<p>Title</p><p>Hello World</p>");
      TestUtils.AreEqualByJson(expect, result);
    }
  }
}
