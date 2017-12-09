using System;
using System.IO;
using System.Collections.Generic;

namespace Haiku
{
    public class WebSite
    {
        private string[] Folders = { "pages", "posts", "public", "template" };
        private string ConfigFile = "config.toml";
        public static Status status = Status.Nothing;


        private bool IsHaikuProject(string path) => (Directory.Exists(path) && File.Exists(Path.Combine(path, ConfigFile)));


        public void New(string path)
        {
            if (IsHaikuProject(path))
                ProjectExists();
            else
                CreateProject(path);
        }


        private void CreateProject(string path)
        {
            Console.WriteLine($"Creating a new project: {path}.\n");

            foreach (var folder in Folders)
            {
                Helper.CreateFolder(path, folder);
                if (folder is "posts" || folder is "pages")
                {
                    var date = DateTime.Today.ToString("yyyy-MM-dd");
                    var filename = folder is "posts" ? $"{date}-hello_world.md" : "about.md";
                    var directory = Path.Combine(path, folder);
                    var filePath = Path.Combine(directory, filename);
                    if (!File.Exists(directory))
                        Helper.WriteSampleResource(filePath);
                }
            }
            Helper.CreateFile(path, ConfigFile);
            ReportProjectCreation(path);
            Helper.DefaultColor();
        }


        private void ReportProjectCreation(string path)
        {
            if (status is Status.Success)
            {
                Helper.GreenText();
                Console.WriteLine($"\nProject \"{path}\" created successfuly.");
                Helper.DefaultColor();
            }
            else
            {
                Helper.RedText();
                Console.WriteLine($"\nAn error ocurred while creating project {path}.");
                Helper.DefaultColor();
            }
        }


        private void ProjectExists()
        {
            Helper.RedText();
            Console.WriteLine("Aborted: Project already exists.");
            Helper.DefaultColor();
        }


        public void Build(string path)
        {
            Console.WriteLine($"Building project: {path}.");
        }
    }
}