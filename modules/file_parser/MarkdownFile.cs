using System.Text.RegularExpressions;
using System.Linq;
using System.IO;
using System.Collections.Generic;

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

      string title = Path.GetFileNameWithoutExtension(filePath);
      string body = "";

      // Separates elements by double newline
      IEnumerable<string> elements = Regex.Split(fileContent, "(\r\r|\n\n|\r\n\r\n)").Where(str => !string.IsNullOrWhiteSpace(str));

      foreach (string element in elements)
      {
        body += MarkdownFile.ParseElement(element);
      }

      return new ParseResult(title, body);
    }

    /// <summary>
    ///   Parse a markdown element (line) to html
    /// </summary>
    private static string ParseElement(string element)
    {

      element = MarkdownFile.ReplaceSingleNewline(element);
      element = MarkdownFile.ParseBoldText(element);
      element = MarkdownFile.ParseHorizontalLine(element);

      return $"<p>{element}</p>";
    }

    /// <summary>
    ///   Replace single newlines with space
    /// </summary>
    private static string ReplaceSingleNewline(string element)
    {
      return Regex.Replace(element, "(\r|\n|\r\n)", " ");
    }

    /// <summary>
    ///   Parse markdown bold to tag `strong`
    /// </summary>
    private static string ParseBoldText(string element)
    {
      // Match anything between two double-asterisk or two double-lodash
      string boldPattern = @"\*{2}(.*?)\*{2}|_{2}(.*?)_{2}";
      string boldReplacement = "<strong>$1</strong>";

      return Regex.Replace(element, boldPattern, boldReplacement);
    }

    /// <summary>
    ///   Parse markdown --- to tag `hr`
    /// </summary>
    private static string ParseHorizontalLine(string element)
    {
      // Match if --- is the only chars in the line
      string hrPattern = @"^---$";
      string hrReplacement = "<hr />";

      return Regex.Replace(element, hrPattern, hrReplacement);
    }
  }
}