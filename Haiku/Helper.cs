using System;
using System.IO;

namespace Haiku
{
    public static class Helper
    {
        public static void CreateFolder(string directory, string foldername)
        {
            var FolderPath = Path.Combine(directory, foldername);
            blueText();
            Console.Write("Creating: ");
            cyanText();
            System.Console.WriteLine(FolderPath);
            grayText();
            Directory.CreateDirectory(FolderPath);
        }

        public static void CreateFile(string directory, string filename)
        {
            var FilePath = Path.Combine(directory, filename);
            blueText();
            Console.Write("Creating: ");
            cyanText();
            Console.WriteLine(FilePath);
            grayText();
            File.Create(FilePath);
        }

        public static void blueText() => Console.ForegroundColor = ConsoleColor.Blue;
        public static void cyanText() => Console.ForegroundColor = ConsoleColor.Cyan;
        public static void greenText() => Console.ForegroundColor = ConsoleColor.Green;
        public static void grayText() => Console.ForegroundColor = ConsoleColor.Gray;
    }
}