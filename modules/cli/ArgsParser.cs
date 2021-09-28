
using System.Collections.Generic;
using CommandLine;

namespace paper_csharp.modules.cli
{

  // Init available options for CLI
  class Options
  {
    [Option('i', "input", HelpText = "Path to file(s)", Required = true)]
    public IEnumerable<string> InputPaths { get; set; }

    [Option('s', "stylesheet", HelpText = "Path to css file or url", Default = "./assets/style.css")]
    public string StylesheetUrl { get; set; }

    [Option('o', "output", HelpText = "Path to output directory", Default = "./dist")]
    public string DistDirPath { get; set; }

    [Option('l', "lang", HelpText = "Locale of generated .html files", Default = "en-CA")]
    public string Lang { get; set; }

  }


  /// <summary>
  ///   Represents the parsed arguments from CLI
  /// </summary>
  public class ArgsParser
  {
    /// <summary>
    ///   output directory
    /// </summary>
    public string DistDirPath { get; private set; }
    /// <summary>
    ///   link to stylesheet
    /// </summary>
    public string StylesheetUrl { get; private set; }
    /// <summary>
    ///   input directories/files
    /// </summary>
    public IEnumerable<string> InputPaths { get; private set; }
    /// <summary>
    ///   The lang of generated .html files
    /// </summary>
    public string Lang { get; private set; }


    public ArgsParser(string[] args)
    {
      Parser.Default
        .ParseArguments<Options>(args)
        .WithParsed<Options>(o =>
        {
          // When it's --help or --version, o.InputPaths is null
          InputPaths = o.InputPaths;

          StylesheetUrl = o.StylesheetUrl;
          DistDirPath = o.DistDirPath;
          Lang = o.Lang;
        });
    }
  }

}