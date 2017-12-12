using System;
using System.IO;
using System.Resources;
using System.Reflection;

namespace Haiku
{
    public enum Status
    {
        Nothing, Error, Success
    };


    public static class Helper
    {
        private static ConsoleColor DefaultConsoleColor = Console.ForegroundColor;


        public static void CreateFolder(string directory, string foldername)
        {
            var FolderPath = Path.Combine(directory, foldername);
            CreateThis("Folder", FolderPath);
        }


        public static void CreateFile(string directory, string filename)
        {
            var FilePath = Path.Combine(directory, filename);
            CreateThis("File", FilePath);
        }


        private static void CreateThis(string Type, string path, StreamReader reader = null)
        {
            CreationMessage(path);
            try
            {
                if (Type is "File")
                    File.Create(path);
                else if (Type is "Folder")
                    Directory.CreateDirectory(path);
                else
                    File.WriteAllText(path, reader.ReadToEnd());
            }
            catch (System.Exception)
            {
                WebSite.status = Status.Error;
            }
            finally
            {
                StatusMessage();
            }
            DefaultColor();
        }


        private static void StatusMessage()
        {
            if (WebSite.status is Status.Error)
            {
                ErrorMessage();
            }
            else
            {
                SuccessMessage();
                WebSite.status = Status.Success;
            }
        }

        private static void CreationMessage(string path)
        {
            BlueText();
            Console.Write("Creating ");
            CyanText();
            Console.Write($"{path}: ");
        }


        public static void WriteSampleResource(string path, string resource, string file)
        {
            var filename = Path.GetFileName(path);
            var assembly = typeof(Haiku.Program).GetTypeInfo().Assembly;
            var resourceName = $"{resource}.{file}";
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            StreamReader reader = new StreamReader(stream);
            CreateThis("Resource", path, reader);
        }


        private static void ErrorMessage()
        {
            RedText();
            Console.WriteLine("Error!");
        }


        private static void SuccessMessage()
        {
            GreenText();
            Console.WriteLine("OK");
        }

        public static void SetColor(ConsoleColor color) => Console.ForegroundColor = color;
        public static void DefaultColor() => SetColor(DefaultConsoleColor);
        public static void BlueText() => SetColor(ConsoleColor.Blue);
        public static void CyanText() => SetColor(ConsoleColor.Cyan);
        public static void GreenText() => SetColor(ConsoleColor.Green);
        public static void RedText() => SetColor(ConsoleColor.Red);
        public static void GrayText() => SetColor(ConsoleColor.Gray);
    }
}