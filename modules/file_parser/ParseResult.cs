namespace paper_csharp.modules.file_parser
{
  /// <summary>
  ///   Represents the result of parsing a file/input
  /// </summary>
  public class ParseResult
  {
    /// <summary>
    ///   The title of the content
    /// </summary>
    public readonly string Title;
    /// <summary>
    ///   The body of the content
    /// </summary>
    public readonly string Body;

    public ParseResult(string title, string body)
    {
      Title = title;
      Body = body;
    }
  }
}