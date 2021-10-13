using paper_csharp.modules.cli;
using System.IO;

namespace paper_csharp.modules.file_parser
{
  /// <summary>
  ///   Represents the parsing options
  /// </summary>
  public class HtmlFileOptions
  {
    public readonly string StylesheetLink;
    public readonly string Lang;

    public HtmlFileOptions(string stylesheetLink, string lang)
    {
      StylesheetLink = stylesheetLink;
      Lang = lang;
    }

    public HtmlFileOptions(CliArgs args) : this(args.StylesheetUrl, args.Lang) { }
  }

  /// <summary>
  ///   Collections of methods for working with .html
  /// </summary>
  public static class HtmlFile
  {
    /// <summary>
    ///   The default content of pages
    /// </summary>
    public static readonly string Template = File.ReadAllText("./assets/template.html");

    /// <summary>
    ///   Parse a content to html content
    /// </summary>
    static public string Parse(ParseResult parseResult, HtmlFileOptions options)
    {
      string result = Template;

      result = result.Replace("$TITLE", parseResult.Title);
      result = result.Replace("$LANG", options.Lang);
      result = result.Replace("$STYLESHEET_LINK", HtmlFile.ParseStylesheetLink(options.StylesheetLink));
      result = result.Replace("$BODY", parseResult.Body);

      return result;
    }

    /// <summary>
    ///   Parse a stylesheet link to html
    /// </summary>
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