// <copyright file="Generator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Paper_csharp.Modules.Cli
{
  using System;
  using System.IO;
  using System.Text;
  using Paper_csharp.Modules.File_parser;
  using Paper_csharp.Modules.Utils;

  /// <summary>
  ///   Represents the statitc site generator.
  /// </summary>
  public class Generator
  {
    public static string SourceStaticDir = @"static";

    public Generator(string[] args)
    {
      this.Args = CliArgs.Parse(args);
    }

    // Parsed arguments from CLI
    public CliArgs Args { get; private set; }

    /// <summary>
    ///   Start generating dist files.
    /// </summary>
    public void Run()
    {
      // Don't run on --help or --version
      if (this.Args == null)
      {
        return;
      }

      this.CreateDistDir();
      this.GenerateDistFiles();
      this.GenerateIndexFile();
      this.GenerateStaticFiles();
    }

    /// <summary>
    ///   Create the default or custom output directory.
    /// </summary>
    private void CreateDistDir()
    {
      DirectoryUtils.CreateDirForce(this.Args.DistDirPath);
    }

    /// <summary>
    ///   Create output files.
    /// </summary>
    private void GenerateDistFiles()
    {
      foreach (string path in this.Args.InputPaths)
      {
        this.GenerateDistFromPath(path);
      }
    }

    /// <summary>
    ///   Create output files from a path.
    /// </summary>
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

    /// <summary>
    ///   Create output files from a directory.
    /// </summary>
    private void GenerateDistFromDir(string dirPath)
    {
      if (!Directory.Exists(dirPath))
      {
        return;
      }

      Directory.CreateDirectory(Path.Join(this.Args.DistDirPath, dirPath));

      string[] subpaths = Directory.GetFileSystemEntries(dirPath);
      foreach (string path in subpaths)
      {
        this.GenerateDistFromPath(path);
      }
    }

    /// <summary>
    ///   Create output file from a file.
    /// </summary>
    private void GenerateDistFromFile(string filePath)
    {
      if (!File.Exists(filePath))
      {
        return;
      }

      try
      {
        ParseResult parseResult = this.ParseFile(filePath);
        string content = HtmlFile.Parse(parseResult, new HtmlFileOptions(this.Args));

        var distPath = Path.Join(this.Args.DistDirPath, Path.GetDirectoryName(filePath), $"{Path.GetFileNameWithoutExtension(filePath)}.html");
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

    /// <summary>
    ///   Parse the content of a file.
    /// </summary>
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
          result = new ParseResult(string.Empty, string.Empty);
          break;
      }

      return result;
    }

    /// <summary>
    ///   Create the index files with links to all html files in the output directory.
    /// </summary>
    private void GenerateIndexFile()
    {
      IndexFile indexFile = new IndexFile(this.Args.DistDirPath);
      indexFile.Generate();
    }

    private void GenerateStaticFiles()
    {
      DirectoryInfo dir = DirectoryUtils.CreateDirSoft(Generator.SourceStaticDir);

      DirectoryUtils.DirectoryCopy(dir.ToString(), Path.Combine(this.Args.DistDirPath, dir.ToString()), true);
    }
  }
}