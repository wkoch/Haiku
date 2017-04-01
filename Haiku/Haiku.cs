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
                    Helper.CreateFolder(BaseDir, folder);
                }
                Helper.CreateFile(BaseDir, ConfigFile);

                Console.WriteLine($"\nProject {BaseDir} created successfuly.");
            }
        }

        public void Build()
        {
            Console.WriteLine("Building this project.");
        }
    }
}