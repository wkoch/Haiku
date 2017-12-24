using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;
using System.Globalization;
using System.Resources;

namespace Haiku
{
    public class WebSite
    {
        public string Name = "Haiku Website";
        public Folder BaseFolder;

        private List<Folder> Folders = new List<Folder>();
        private List<Page> Pages = new List<Page>();
        private List<Post> Posts = new List<Post>();
        private List<Template> Templates = new List<Template>();

        private void UpdateName()
        {
            if (Program.CLI.OptionWasGiven())
                Name = Program.CLI.Option;
            BaseFolder = new Folder(Name, Name);
        }

        public void New()
        {
            UpdateName();
            WriteResources();
            Console.WriteLine($"Created new project in \"{BaseFolder.Name}\".");
        }

        private void WriteResources()
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var resourceName in assembly.GetManifestResourceNames())
            {
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    var filePattern = @"((\d*-\d*-\d*-)?\w+\.\w+)$";
                    var fileMatch = new Regex(filePattern, RegexOptions.IgnoreCase);
                    var file = fileMatch.Match(resourceName).Groups[1].ToString();

                    var folderPattern = @"Haiku\.Resources\.(\w+)\.((\d*-\d*-\d*-)?\w+\.\w+)$";
                    var folderMatch = new Regex(folderPattern, RegexOptions.IgnoreCase);
                    var folder = folderMatch.Match(resourceName).Groups[1].ToString();

                    System.IO.Directory.CreateDirectory(Path.Combine(BaseFolder.Name, folder));
                    var reader = new StreamReader(stream);
                    System.IO.File.WriteAllText(Path.Combine(BaseFolder.Name, folder, file), reader.ReadToEnd());
                }
            }
        }

        public void Build()
        {
            UpdateName();
            System.Console.WriteLine($"Building \"{BaseFolder.Name}\".");
            ReadFolders();
            ReadProjectFiles();
            BuildProject();
        }

        private void ReadProjectFiles()
        {
            foreach (var folder in Folders)
            {
                switch (folder.Name)
                {
                    case "pages":
                        var files = ListFiles(folder.Path);
                        foreach (var file in files)
                        {
                            Pages.Add(new Page { Name = file, Path = Path.Combine(folder.Path, file)});
                        }
                        break;
                    default:
                        break;
                }
            }
            // ReadTemplates();
            // ReadPages();
            // ReadPosts();
        }

        private string[] ListFiles(string path)
        {
            return System.IO.Directory.GetFiles(path);
        }

        private void ReadFolders()
        {
            var folders = System.IO.Directory.GetDirectories(BaseFolder.Path);
            foreach (var folder in folders)
            {
                var name = System.IO.Path.GetFileName(folder);
                Folders.Add(new Folder(name, folder));
            }
        }

        private void BuildProject()
        {
            // BuildIndex();
            // BuildPages();
            // BuildPosts();
            // CopyStaticFiles();
        }
    }
}