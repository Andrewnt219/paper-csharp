using System;
using System.Runtime.InteropServices;
using System.IO;

namespace paper_csharp.modules.file_parser
{
  public class HtmlFileOptions
  {
    public readonly string StylesheetLink;

    public HtmlFileOptions(string stylesheetLink)
    {
      this.StylesheetLink = stylesheetLink;
    }
  }
  public static class HtmlFile
  {
    public static readonly string Template = File.ReadAllText("./assets/template.html");
    static public string Parse(ParseResult parseResult, HtmlFileOptions options)
    {
      string result = Template;

      result = result.Replace("$TITLE", parseResult.Title);
      result = result.Replace("$BODY", parseResult.Body);
      if (!string.IsNullOrWhiteSpace(options?.StylesheetLink))
      {
        result = result.Replace("$STYLESHEET_LINK", HtmlFile.ParseStylesheetLink(options.StylesheetLink));

      }

      return result;
    }

    static private string ParseStylesheetLink(string stylesheetLink)
    {
      if (string.IsNullOrWhiteSpace(stylesheetLink))
      {
        return "";
      }

      if (File.Exists(stylesheetLink))
      {
        string stylesheetContent = File.ReadAllText(stylesheetLink);
        return $"<style>{stylesheetContent}</style>";
      }

      return $"<link rel='stylesheet' href='{stylesheetLink}' />";
    }
  }
}