using System;
using paper_csharp.modules.cli;

namespace paper_csharp
{
  class Program
  {
    static void Main(string[] args)
    {
      ArgsParser parser = new ArgsParser(args);
      Console.WriteLine($"{parser.DistDirPath} {parser.InputPaths} {parser.StylesheetUrl}");
    }
  }
}
