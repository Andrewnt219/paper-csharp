

using System;
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

      Console.WriteLine($"{Args.DistDirPath} {Args.InputPaths} {Args.StylesheetUrl}");

    }

  }
}