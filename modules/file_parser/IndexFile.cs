using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace paper_csharp.modules.file_parser
{

  /// <summary>
  ///   A collection of methods to generate an index file for a given dir path
  /// </summary>
  public class IndexFile
  {
    /// <summary>
    ///   Path to the directory with .html files to generate index
    /// </summary>
    public string SourceDirPath;

    public IndexFile(string sourceDirPath)
    {
      SourceDirPath = sourceDirPath;
    }


    /// <summary>
    ///   Create the index files with links to all html files in the output directory
    /// </summary>
    public void Generate()
    {
      var indexFile = File.Create(Path.Join(this.SourceDirPath, "index.html"));

      var linkList = ReadAllFilePaths(this.SourceDirPath).Select(filePath => $"<a style=\"display:block\" href=\"{filePath}\">{Path.GetFileNameWithoutExtension(filePath)}</a>");

      indexFile.Write(Encoding.ASCII.GetBytes(string.Join("\n", linkList)));

      indexFile.Close();
      return;
    }


    /// <summary>
    ///   Returns all paths to .html files
    /// </summary>
    private List<string> ReadAllFilePaths(string path)
    {
      List<string> paths = new List<string>();

      foreach (string subpath in Directory.GetFileSystemEntries(path))
      {
        if (Directory.Exists(subpath))
        {
          paths.AddRange(this.ReadAllFilePaths(subpath));
          continue;
        }

        if (File.Exists(subpath) && Path.GetExtension(subpath) == ".html" && Path.GetFileName(subpath) != "index.html")
        {
          paths.Add(Path.GetRelativePath(this.SourceDirPath, subpath));
          continue;
        }

      }

      return paths;
    }
  }
}