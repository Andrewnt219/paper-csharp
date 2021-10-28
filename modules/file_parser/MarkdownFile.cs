using System;
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
      MarkdownParser parser = new MarkdownParser(element);

      parser.ReplaceSingleNewline().ParseBoldText().ParseHorizontalLine().ParseImage();

      return $"<p>{parser.Element}</p>";
    }


  }

  public class MarkdownParser
  {
    public string Element;

    public MarkdownParser(string element)
    {
      Element = element;
    }

    /// <summary>
    ///   Replace single newlines with space
    /// </summary>
    public MarkdownParser ReplaceSingleNewline()
    {
      Element = Regex.Replace(Element, "(\r|\n|\r\n)", " ");

      return this;
    }

    /// <summary>
    ///   Parse markdown bold to tag `strong`
    /// </summary>
    public MarkdownParser ParseBoldText()
    {
      // Match anything between two double-asterisk or two double-lodash
      string boldPattern = @"\*{2}(.*?)\*{2}|_{2}(.*?)_{2}";
      string boldReplacement = "<strong>$1</strong>";

      Element = Regex.Replace(Element, boldPattern, boldReplacement);

      return this;
    }

    /// <summary>
    ///   Parse markdown --- to tag `hr`
    /// </summary>
    public MarkdownParser ParseHorizontalLine()
    {
      // Match if --- is the only chars in the line
      string hrPattern = @"^---$";
      string hrReplacement = "<hr />";

      Element = Regex.Replace(Element, hrPattern, hrReplacement);

      return this;
    }

    /// <summary>
    ///   Parse markdown [alt](src) to tag `img`
    /// </summary>
    public MarkdownParser ParseImage()
    {
      string imgPattern = @"\[(?<alt>.*)\]\((?<src>.*)\)";
      var match = Regex.Match(Element, imgPattern);

      if (match != null)
      {
        Element = $"<img alt=\"{match.Groups["alt"]}\" src=\"{match.Groups["src"]}\" />";
      }

      return this;
    }
  }
}