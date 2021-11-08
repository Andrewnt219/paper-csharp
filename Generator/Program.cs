// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Paper_csharp
{
  using System;
  using Paper_csharp.Modules.Cli;

  internal class Program
  {
    private static void Main(string[] args)
    {
      Generator generator = new Generator(args);
      generator.Run();
    }
  }
}
