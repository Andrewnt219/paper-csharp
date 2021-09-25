using System;
using paper_csharp.modules.cli;

namespace paper_csharp
{
  class Program
  {
    static void Main(string[] args)
    {
      Generator generator = new Generator(args);
      generator.Run();
    }
  }
}
