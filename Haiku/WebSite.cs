using System;
using System.IO;
using System.Collections.Generic;

namespace Haiku
{
    public class WebSite
    {
        private string[] Folders = { "pages", "posts", "public", "template" };
        private string ConfigFile = "config.toml";
        private string[] TemplateFiles = {
            "_blog_index.html",
            "_footer.html",
            "_header.html",
            "_menu.html",
            "_page.html",
            "layout.html"};
        public static Status status = Status.Nothing;


        private bool IsHaikuProject(string path) => (Directory.Exists(path) && File.Exists(Path.Combine(path, ConfigFile)));


        public void New(string path)
        {
            if (IsHaikuProject(path))
                AbortMessage("Project already exists.");
            else
                CreateProject(path);
        }


        private void CreateProject(string path)
        {
            Console.WriteLine($"Creating a new project: {path}.\n");

            foreach (var folder in Folders)
            {
                Helper.CreateFolder(path, folder);
                if (folder is "pages" || folder is "posts")
                {
                    var filename = "";
                    var file = "";

                    if (folder is "posts")
                    {
                        var date = DateTime.Today.ToString("yyyy-MM-dd");
                        file = "hello_world.md";
                        filename = $"{date}-{file}";
                    }
                    else if (folder is "pages")
                    {
                        file = "about.md";
                        filename = $"{file}";
                    }
                    var directory = Path.Combine(path, folder);
                    var filePath = Path.Combine(directory, filename);
                    if (!File.Exists(directory))
                        Helper.CreateResource(filePath, "Haiku.Resources.Examples", file);
                }
            }
            foreach (var file in TemplateFiles)
            {
                var filepath = Path.Combine(path, Folders[Folders.Length - 1], file);
                var directory = Path.Combine(path, Folders[Folders.Length - 1]);
                Helper.CreateResource(directory, "Haiku.Resources.Template", file);
                // Helper.CreateFile(folderpath, file);
            }
            Helper.CreateFile(path, ConfigFile);
            ReportProjectCreation(path);
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


        private void AbortMessage(string msg)
        {
            Helper.RedText();
            Console.WriteLine($"Aborted: {msg}");
            Helper.DefaultColor();
        }


        public void Build(string path)
        {
            if (IsHaikuProject(path))
            {
                Console.WriteLine($"Building project: {path}.");
                ProcessPosts("HaikuWebsite/posts", "HaikuWebsite/public");
            }
            else
                AbortMessage("Haiku project not found.");
        }


        private IEnumerable<string> ListContent(string folder) => Directory.EnumerateFiles(folder, "*.md");

        private void ProcessPosts(string source, string destination)
        {
            foreach (var file in ListContent(source))
            {
                var content = File.ReadAllText(file);
                var html = "<p>@post</p>".Replace("@post", content);
                var filename = Path.GetFileName(file);
                var newFile = Path.ChangeExtension(Path.Combine(destination, filename), "html");
                File.WriteAllText(newFile, html);
            }
        }
    }
}