
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace paper_csharp.modules.file_parser
{
  public static class TextFile
  {
    static public ParseResult Parse(List<string> lines)
    {
      string title = "";
      string body = "";
      ushort blankLineCount = 0;

      for (int i = 0; i < lines.Count; i++)
      {
        var line = lines[i];

        if (i == 0)
        {
          title += line;
        }
        else if (string.IsNullOrWhiteSpace(line) && i <= 2)
        {
          blankLineCount++;
        }
        else
        {
          body += TextFile.ParseBodyLine(line);
        }
      }

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