using System;
using System.IO;
using System.Collections.Generic;

namespace Haiku
{
    public class WebSite
    {
        private string[] Folders = { "pages", "posts", "public", "template" };
        private string ConfigFile = "config.toml";

        private bool IsHaikuProject(string path) => (Directory.Exists(path) && File.Exists(Path.Combine(path, ConfigFile)));

        public void New(string path)
        {
            var defaultColor = Console.ForegroundColor;
            if (IsHaikuProject(path))
            {
                Console.WriteLine("Aborting: Project already exists.");
            }
            else
            {
                Console.WriteLine($"Creating a new project on \"{path}\".\n");

                foreach (var folder in Folders)
                {
                    Helper.CreateFolder(path, folder);
                    if (folder == "posts" || folder == "pages")
                    {
                        DateTime today = DateTime.Today;
                        var filename = folder == "posts" ? $"{today.Year}-{today.Month}-{today.Day}-hello-world.md" : "about.md";
                        var directory = Path.Combine(path, folder);
                        if (!File.Exists(directory))
                        {
                            Helper.WriteSampleResource(directory, filename);
                        }
                    }
                }
                Helper.CreateFile(path, ConfigFile);
                Helper.greenText();
                Console.WriteLine($"\nProject \"{path}\" created successfuly.");
                Helper.setColor(defaultColor);
            }
        }

        public void Build(string path)
        {
            Console.WriteLine($"Building project on \"{path}\".");
        }
    }
}