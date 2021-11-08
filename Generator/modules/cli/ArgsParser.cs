// <copyright file="ArgsParser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Paper_csharp.Modules.Cli
{
  using System;
  using System.Collections.Generic;
  using CommandLine;

  // Init available options for CLI
  public class CliArgs
  {
    [Option('i', "input", HelpText = "Path to file(s)", Required = true)]
    public IEnumerable<string> InputPaths { get; set; }

    [Option('s', "stylesheet", HelpText = "Path to css file or url", Default = "./assets/style.css")]
    public string StylesheetUrl { get; set; }

    [Option('o', "output", HelpText = "Path to output directory", Default = "./dist")]
    public string DistDirPath { get; set; }

    [Option('l', "lang", HelpText = "Locale of generated .html files", Default = "en-CA")]
    public string Lang { get; set; }

    public static CliArgs Parse(string[] args)
    {
      CliArgs options = null;

      Parser.Default.ParseArguments<CliArgs>(args)
        .WithParsed(opts => options = opts)
        .WithNotParsed(CliArgs.HandleParseError);

      return options;
    }

    private static void HandleParseError(IEnumerable<Error> errs)
    {
      if (errs.IsVersion())
      {
        Console.WriteLine("Version Request");
        return;
      }

      if (errs.IsHelp())
      {
        Console.WriteLine("Help Request");
        return;
      }

      Console.WriteLine("Parser Fail");
    }
  }
}