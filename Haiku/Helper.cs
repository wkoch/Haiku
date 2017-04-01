using System;
using System.IO;

namespace Haiku
{
    public static class Helper
    {
        public static void CreateFolder(string directory, string foldername)
        {
            Console.WriteLine($"Creating folder: {Path.Combine(directory, foldername)}");
            Directory.CreateDirectory(Path.Combine(directory, foldername));
        }

        public static void CreateFile(string directory, string filename)
        {
            Console.WriteLine($"Creating file: {Path.Combine(directory, filename)}");
            File.Create(Path.Combine(directory, filename));
        }
    }
}