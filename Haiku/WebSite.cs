using System;
using System.IO;
using System.Collections.Generic;

namespace Haiku
{
    public class WebSite
    {
        private string[] Folders = { "pages", "posts", "public", "template" };
        private string ConfigFile = "config.toml";
        private static ConsoleColor DefaultColor = Console.ForegroundColor;
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
                    DateTime today = DateTime.Today;
                    string year = today.Year.ToString();
                    string month = today.Month.ToString().PadLeft(2, '0');
                    string day = today.Day.ToString().PadLeft(2, '0');
                    string date = $"{year}-{month}-{day}";
                    var filename = folder is "posts" ? $"{date}-hello_world.md" : "about.md";
                    var directory = Path.Combine(path, folder);
                    var filePath = Path.Combine(directory, filename);
                    if (!File.Exists(directory))
                        Helper.WriteSampleResource(filePath);
                }
            }
            Helper.CreateFile(path, ConfigFile);
            ReportProjectCreation(path);
            Helper.setColor(DefaultColor);
        }


        private void ReportProjectCreation(string path)
        {
            if (status is Status.Success)
            {
                Helper.greenText();
                Console.WriteLine($"\nProject \"{path}\" created successfuly.");
            }
            else
            {
                Helper.redText();
                Console.WriteLine($"\nAn error ocurred while creating project {path}.");
            }
        }


        private void ProjectExists()
        {
            Helper.redText();
            Console.WriteLine("Aborted: Project already exists.");
            Helper.setColor(DefaultColor);
        }


        public void Build(string path)
        {
            Console.WriteLine($"Building project: {path}.");
        }
    }
}