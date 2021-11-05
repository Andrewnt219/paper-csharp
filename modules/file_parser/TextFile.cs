// <copyright file="TextFile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Paper_csharp.Modules.File_parser
{
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;

  /// <summary>
  ///   A collection of methods for working with .txt file.
  /// </summary>
  public static class TextFile
  {
    /// <summary>
    ///   Parse the content of file to sections/parts.
    /// </summary>
    /// <returns></returns>
    public static ParseResult Parse(string filePath)
    {
      List<string> lines = File.ReadAllLines(filePath).ToList();

      string title = string.Empty;
      string body = string.Empty;
      ushort blankLineCount = 0;

      for (int i = 0; i < lines.Count; i++)
      {
        var line = lines[i];

        // First line can  be title
        if (i == 0)
        {
          title += line;
        }

        // If the 2nd and 3rd line are blank then 1st line will be a title
        else if (string.IsNullOrWhiteSpace(line) && i <= 2)
        {
          blankLineCount++;
        }

        // The rest is the  body
        else
        {
          body += TextFile.ParseBodyLine(line);
        }
      }

      // Check if the source has a title or not
      if (blankLineCount == 2)
      {
        body = $"<h1>{title}</h1>{body}";
      }
      else
      {
        body = TextFile.ParseBodyLine(title) + body;
        title = string.Empty;
      }

      return new ParseResult(title, body);
    }

    /// <summary>
    ///   Parse lines into html.
    /// </summary>
    private static string ParseBodyLine(string line)
    {
      if (string.IsNullOrWhiteSpace(line))
      {
        return string.Empty;
      }

      return $"<p>{line}</p>";
    }
  }
}