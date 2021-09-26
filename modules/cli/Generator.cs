

using System.Text.RegularExpressions;
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
      this.GenerateIndexFile();
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
        ParseResult parseResult = this.ParseFile(filePath);
        string content = HtmlFile.Parse(parseResult, new HtmlFileOptions(Args.StylesheetUrl));

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

    private ParseResult ParseFile(string filePath)
    {
      ParseResult result;

      switch (Path.GetExtension(filePath))
      {
        case ".txt":
          result = TextFile.Parse(filePath);
          break;

        case ".md":
          result = MarkdownFile.Parse(filePath);
          break;

        default:
          result = new ParseResult("", "");
          break;
      }

      return result;
    }



    private void GenerateIndexFile()
    {
      var indexFile = File.Create(Path.Join(Args.DistDirPath, "index.html"));

      var linkList = ReadAllFilePaths(Args.DistDirPath).Select(filePath => $"<a style=\"display:block\" href=\"{filePath}\">{Path.GetFileNameWithoutExtension(filePath)}</a>");

      indexFile.Write(Encoding.ASCII.GetBytes(string.Join("\n", linkList)));

      indexFile.Close();
      return;
    }

    private List<string> ReadAllFilePaths(string path)
    {
      List<string> paths = new List<string>();

      foreach (string subpath in Directory.GetFileSystemEntries(path))
      {
        if (Directory.Exists(subpath))
        {
          paths.AddRange(this.ReadAllFilePaths(subpath));
          continue;
        }

        if (File.Exists(subpath) && Path.GetExtension(subpath) == ".html" && Path.GetFileName(subpath) != "index.html")
        {
          paths.Add(Path.GetRelativePath(Args.DistDirPath, subpath));
          continue;
        }

      }

      return paths;
    }
  }
}