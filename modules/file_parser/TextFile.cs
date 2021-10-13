
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace paper_csharp.modules.file_parser
{
  /// <summary>
  ///   A collection of methods for working with .txt file
  /// </summary>
  public static class TextFile
  {
    /// <summary>
    ///   Parse the content of file to sections/parts
    /// </summary>
    static public ParseResult Parse(string filePath)
    {
      List<string> lines = File.ReadAllLines(filePath).ToList();

      string title = "";
      string body = "";
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
        title = "";
      }

      return new ParseResult(title, body);
    }


    /// <summary>
    ///   Parse lines into html
    /// </summary>
    static private string ParseBodyLine(string line)
    {
      if (string.IsNullOrWhiteSpace(line))
      {
        return "";
      }

      return $"<p>{line}</p>";
    }

  }
}