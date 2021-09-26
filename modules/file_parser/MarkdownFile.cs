using System.Text.RegularExpressions;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace paper_csharp.modules.file_parser
{
  public static class MarkdownFile
  {
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

    private static string ParseElement(string element)
    {

      element = MarkdownFile.ReplaceSingleNewline(element);
      element = MarkdownFile.ParseBoldText(element);


      return $"<p>{element}</p>";
    }

    private static string ReplaceSingleNewline(string element)
    {
      return Regex.Replace(element, "(\r|\n|\r\n)", " ");
    }

    private static string ParseBoldText(string element)
    {
      // Match anything between two double-asterisk or two double-lodash
      string boldPattern = @"\*{2}(.*?)\*{2}|_{2}(.*?)_{2}";
      string boldReplacement = "<strong>$1</strong>";

      return Regex.Replace(element, boldPattern, boldReplacement);
    }
  }
}