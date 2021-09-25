using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paper_csharp.modules.file_parser
{
  public class ParseResult
  {
    public readonly string Title;
    public readonly string Body;

    public ParseResult(string title, string body)
    {
      Title = title;
      Body = body;
    }
  }
}