// <copyright file="DirectoryUtils.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Paper_csharp.Modules.Utils
{
  using System.IO;

  public class DirectoryUtils
  {
    // Overwrite or create a new directory at path
    public static DirectoryInfo CreateDirForce(string path)
    {
      if (Directory.Exists(path))
      {
        Directory.Delete(path, true);
      }

      return Directory.CreateDirectory(path);
    }

    // Create a new directory if not exist at path
    public static DirectoryInfo CreateDirSoft(string path)
    {
      DirectoryInfo dir = new DirectoryInfo(path);

      if (dir.Exists)
      {
        return dir;
      }

      return Directory.CreateDirectory(dir.ToString());
    }

    public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
      // Get the subdirectories for the specified directory.
      DirectoryInfo dir = new DirectoryInfo(sourceDirName);

      if (!dir.Exists)
      {
        throw new DirectoryNotFoundException(
            "Source directory does not exist or could not be found: "
            + sourceDirName);
      }

      DirectoryInfo[] dirs = dir.GetDirectories();

      // If the destination directory doesn't exist, create it.
      Directory.CreateDirectory(destDirName);

      // Get the files in the directory and copy them to the new location.
      FileInfo[] files = dir.GetFiles();
      foreach (FileInfo file in files)
      {
        string tempPath = Path.Combine(destDirName, file.Name);
        file.CopyTo(tempPath, false);
      }

      // If copying subdirectories, copy them and their contents to new location.
      if (copySubDirs)
      {
        foreach (DirectoryInfo subdir in dirs)
        {
          string tempPath = Path.Combine(destDirName, subdir.Name);
          DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
        }
      }
    }
  }
}