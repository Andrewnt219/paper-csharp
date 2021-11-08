// <copyright file="ParseResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Paper_csharp.Modules.File_parser
{
  /// <summary>
  ///   Represents the result of parsing a file/input.
  /// </summary>
  public class ParseResult
  {
    /// <summary>
    ///   The title of the content.
    /// </summary>
    public readonly string Title;

    /// <summary>
    ///   The body of the content.
    /// </summary>
    public readonly string Body;

    public ParseResult(string title, string body)
    {
      this.Title = title;
      this.Body = body;
    }
  }
}