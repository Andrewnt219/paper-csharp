using System.Text.RegularExpressions;
using Markdig;
using System.IO;

namespace paper_csharp.modules.file_parser
{
  /// <summary>
  ///   A collection of methods to work with .md files
  /// </summary>
  public static class MarkdownFile
  {

    /// <summary>
    ///   Parse the content of a file to different sections (parts)
    /// </summary>
    static public ParseResult Parse(string filePath)
    {
      string fileContent = File.ReadAllText(filePath);
      string staticAssetPatter = @"!\[(.*)\]\((.*)\)";

      string title = Path.GetFileNameWithoutExtension(filePath);
      // Rewrite static asssets
      string body = Markdown.ToHtml(Regex.Replace(fileContent, staticAssetPatter, "![$1](/$2)"));

      return new ParseResult(title, body);
    }
  }
}