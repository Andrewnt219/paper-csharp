using System.Security;
using System.Globalization;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using CommandLine;

namespace paper_csharp.modules.cli
{

  class Options
  {
    [Option('i', "input", HelpText = "Path to file(s)", Required = true)]
    public IEnumerable<string> InputPaths { get; set; }

    [Option('s', "stylesheet", HelpText = "Path to css file or url", Default = "./assets/style.css")]
    public string StylesheetUrl { get; set; }

    [Option('o', "output", HelpText = "Path to output directory", Default = "./dist")]
    public string DistDirPath { get; set; }

  }

  public class ArgsParser
  {
    public string DistDirPath { get; private set; }
    public string StylesheetUrl { get; private set; }
    public IEnumerable<string> InputPaths { get; private set; }

    public ArgsParser(string[] args)
    {
      Parser.Default
        .ParseArguments<Options>(args)
        .WithParsed<Options>(o =>
        {
          // When it's --help or --version, o.InputPaths is null
          InputPaths = o.InputPaths ?? Enumerable.Empty<string>();

          StylesheetUrl = o.StylesheetUrl;
          DistDirPath = o.DistDirPath;
        });
    }
  }

}