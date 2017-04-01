using System;
using System.IO;

namespace Haiku
{
    public static class Helper
    {
        public static void CreateFolder(string directory, string foldername)
        {
            var FolderPath = Path.Combine(directory, foldername);
            Console.WriteLine($"Creating folder: {FolderPath}");
            Directory.CreateDirectory(FolderPath);
        }

        public static void CreateFile(string directory, string filename)
        {
            var FilePath = Path.Combine(directory, filename);
            Console.WriteLine($"Creating file: {FilePath}");
            File.Create(FilePath);
        }
    }
}