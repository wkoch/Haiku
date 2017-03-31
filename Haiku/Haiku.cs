using System;
using System.IO;

namespace Haiku
{
    public class Haiku
    {
        public string BaseDir { get; set; }
        public string[] Folders = { "pages", "posts", "public", "template" };
        public string ConfigFile = "config.toml";

        public Haiku(string baseDir = "HaikuWebsite")
        {
            BaseDir = baseDir;
        }

        private bool IsHaikuProject()
        {
            if (Directory.Exists(BaseDir) && File.Exists(Path.Combine(BaseDir, ConfigFile)))
                return true;
            else
                return false;
        }

        public void New()
        {
            if (IsHaikuProject())
            {
                Console.WriteLine("Error! Project already exists.");
            }
            else
            {
                Console.WriteLine($"Creating a new project on {BaseDir}.\n");

                foreach (string folder in Folders)
                {
                    CreateFolder(BaseDir, folder);
                }
                CreateFile(BaseDir, ConfigFile);

                Console.WriteLine($"\nProject {BaseDir} created successfuly.");
            }
        }

        public void Build()
        {
            Console.WriteLine("Building this project.");
        }

        private void CreateFolder(string directory, string foldername)
        {
            Console.WriteLine($"Creating folder: {Path.Combine(directory, foldername)}");
            Directory.CreateDirectory(Path.Combine(directory, foldername));
        }

        private void CreateFile(string directory, string filename)
        {
            Console.WriteLine($"Creating file: {Path.Combine(directory, filename)}");
            File.Create(Path.Combine(directory, filename));
        }
    }
}