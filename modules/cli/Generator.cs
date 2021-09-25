

using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using paper_csharp.modules.file_parser;
namespace paper_csharp.modules.cli
{

  public class Generator
  {
    public ArgsParser Args { get; private set; }

    public Generator(string[] args)
    {
      Args = new ArgsParser(args);
    }

    public void Run()
    {
      this.CreateDistDir();
      this.GenerateDistFiles();
    }

    private void CreateDistDir()
    {
      if (Directory.Exists(Args.DistDirPath))
      {
        Directory.Delete(Args.DistDirPath, true);
      }

      Directory.CreateDirectory(Args.DistDirPath);
    }

    private void GenerateDistFiles()
    {
      foreach (string path in Args.InputPaths)
      {
        this.GenerateDistFromPath(path);
      }

    }

    private void GenerateDistFromPath(string path)
    {
      if (Directory.Exists(path))
      {
        this.GenerateDistFromDir(path);
        return;
      }

      if (File.Exists(path))
      {
        this.GenerateDistFromFile(path);
        return;
      }

      System.Console.WriteLine($"Path is not recognized: {path}");
      System.Environment.Exit(1);
      return;
    }

    private void GenerateDistFromDir(string dirPath)
    {
      if (!Directory.Exists(dirPath))
      {
        return;
      }


      Directory.CreateDirectory(Path.Join(Args.DistDirPath, dirPath));

      string[] subpaths = Directory.GetFileSystemEntries(dirPath);
      foreach (string path in subpaths)
      {
        this.GenerateDistFromPath(path);
      }
    }

    private void GenerateDistFromFile(string filePath)
    {
      if (!File.Exists(filePath))
      {
        return;
      }

      try
      {
        List<string> lines = File.ReadAllLines(filePath).ToList();
        ParseResult result = TextFile.Parse(lines);
        string content = HtmlFile.Parse(result);

        var distPath = Path.Join(Args.DistDirPath, Path.GetDirectoryName(filePath), $"{Path.GetFileNameWithoutExtension(filePath)}.html");
        var htmlFile = File.Create(distPath);
        htmlFile.Write(Encoding.ASCII.GetBytes(content));
        htmlFile.Close();
      }
      catch (Exception ex)
      {
        System.Console.WriteLine($"Fail to generate dist files: {ex.Message}");
        System.Environment.Exit(1);
      }

    }
  }


}