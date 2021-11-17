// <copyright file="GeneratorTester.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Generator.Tests
{
  using System.Data;
  using System.IO;
  using System.Text;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Paper_csharp.Modules.Cli;

  [TestClass]
  public class GeneratorTester
  {
    private string txtFilePath;
    private string txtFileDistPath;
    private string mdFilePath;
    private string mdFileDistPath;
    private string distPath;
    private string styleSheetUrl;

    [TestInitialize]
    public void CreateFiles()
    {
      this.distPath = Path.GetRandomFileName();
      this.styleSheetUrl = "https://cdn.jsdelivr.net/npm/water.css@2/out/water.css";

      this.txtFilePath = Path.GetRandomFileName() + ".txt";
      this.txtFileDistPath = Path.Combine(this.distPath, Path.GetFileNameWithoutExtension(this.txtFilePath) + ".html");
      using (System.IO.FileStream fs = System.IO.File.Create(this.txtFilePath))
      {
        fs.Write(Encoding.ASCII.GetBytes("Title\n\n\nHello World"));
      }

      this.mdFilePath = Path.GetRandomFileName() + ".md";
      this.mdFileDistPath = Path.Combine(this.distPath, Path.GetFileNameWithoutExtension(this.mdFilePath) + ".html");
      using (System.IO.FileStream fs = System.IO.File.Create(this.mdFilePath))
      {
        fs.Write(Encoding.ASCII.GetBytes("#Title\nHello World"));
      }
    }

    [TestCleanup]
    public void DeleteFiles()
    {
      File.Delete(this.txtFilePath);
      File.Delete(this.mdFilePath);
      Directory.Delete(this.distPath, true);
    }

    [TestMethod]
    public void RunWithSingleFile()
    {
      string content = File.ReadAllText(this.txtFilePath);
      string[] args = { "-i", this.txtFilePath, "-o", this.distPath, "-s", this.styleSheetUrl };
      var generator = new Generator(args);
      generator.Run();

      Assert.IsTrue(Directory.Exists(this.distPath));
      Assert.IsTrue(File.Exists(this.txtFileDistPath));
    }

    [TestMethod]
    public void RunWithMultipleFiles()
    {
      string content = File.ReadAllText(this.txtFilePath);
      string[] args = { "-i", this.txtFilePath, this.mdFilePath, "-o", this.distPath, "-s", this.styleSheetUrl };
      var generator = new Generator(args);
      generator.Run();

      Assert.IsTrue(Directory.Exists(this.distPath));
      Assert.IsTrue(File.Exists(this.txtFileDistPath));
      Assert.IsTrue(File.Exists(this.mdFileDistPath));
    }
  }
}