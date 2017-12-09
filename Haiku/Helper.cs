using System;
using System.IO;
using System.Resources;
using System.Reflection;

namespace Haiku
{
    public enum Status
    {
        Nothing,
        Error,
        Success
    };


    public static class Helper
    {
        private static ConsoleColor DefaultColor = Console.ForegroundColor;


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
            setColor(DefaultColor);
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
            blueText();
            Console.Write("Creating ");
            cyanText();
            Console.Write($"{path}: ");
        }


        public static void WriteSampleResource(string path)
        {
            var filename = Path.GetFileName(path);
            var assembly = typeof(Haiku.Program).GetTypeInfo().Assembly;
            var resourceName = "Haiku.Resources.Examples.Lorem.md";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            CreateThis("Resource", path, reader);
        }


        private static void ErrorMessage()
        {
            redText();
            Console.WriteLine("Error!");
        }


        private static void SuccessMessage()
        {
            greenText();
            Console.WriteLine("OK");
        }

        public static void setColor(ConsoleColor color) => Console.ForegroundColor = color;
        public static void blueText() => setColor(ConsoleColor.Blue);
        public static void cyanText() => setColor(ConsoleColor.Cyan);
        public static void greenText() => setColor(ConsoleColor.Green);
        public static void redText() => setColor(ConsoleColor.Red);
        public static void grayText() => setColor(ConsoleColor.Gray);

    }
}