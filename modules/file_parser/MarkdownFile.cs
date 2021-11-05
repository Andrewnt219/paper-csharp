// <copyright file="MarkdownFile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Paper_csharp.Modules.File_parser
{
  using System.IO;
  using System.Text.RegularExpressions;
  using Markdig;

  /// <summary>
  ///   A collection of methods to work with .md files.
  /// </summary>
  public static class MarkdownFile
  {
    /// <summary>
    ///   Parse the content of a file to different sections (parts).
    /// </summary>
    /// <returns></returns>
    public static ParseResult Parse(string filePath)
    {
      string fileContent = File.ReadAllText(filePath);
      string staticAssetPatter = @"!\[(.*)\]\((.*)\)";

      string title = Path.GetFileNameWithoutExtension(filePath);

      // Rewrite static asssets
      string body = Markdown.ToHtml(Regex.Replace(fileContent, staticAssetPatter, "![$1](/static/$2)"));

      return new ParseResult(title, body);
    }
  }
}