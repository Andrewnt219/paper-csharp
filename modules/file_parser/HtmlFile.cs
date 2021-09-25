using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Resources;
using System.Reflection;
namespace paper_csharp.modules.file_parser
{
  public static class HtmlFile
  {
    public static readonly string Template = File.ReadAllText("./assets/template.html");
    static public string Parse(ParseResult parseResult)
    {
      string result = Template;

      result = result.Replace("$TITLE", parseResult.Title);
      result = result.Replace("$BODY", parseResult.Body);

      return result;
    }
  }
}