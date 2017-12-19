using System;
using System.IO;
using System.Collections.Generic;

namespace Haiku
{
    public class WebSite
    {
        private Folder _baseFolder;
        readonly private string[] _folders = { "pages", "posts", "public", "template" };
        private (string folder, string file)[] _resources =
        {
            (folder: "pages", file: "about.md"),
            (folder: "posts", file: "2017-12-20-hello_world.md"),
            (folder: "template", file: "_blog_index.html"),
            (folder: "template", file: "_footer.html"),
            (folder: "template", file: "_header.html"),
            (folder: "template", file: "_menu.html"),
            (folder: "template", file: "_page.html"),
            (folder: "template", file: "layout.html"),
        };
        private Config _config;
        private Status _status = Status.Nothing;

        private bool IsHaikuProject() => (_baseFolder.Exists() && _config.File.Exists());


        public WebSite(string folder)
        {
            var baseFolderName = (folder != null) ? folder : "HaikuWebsite";
            _baseFolder = new Folder(null, baseFolderName);
            _config = new Config(_baseFolder, Program.AppName, "conf");
        }

        public void New()
        {
            foreach (var _folder in _folders)
            {
                var dir = new Folder(_baseFolder, _folder);
                _baseFolder.Folders.Add(dir);
                foreach (var resource in _resources)
                {
                    if (_folder == resource.folder)
                    {
                        dir.Files.Add(File.FromResource(new File(dir, resource.file)));
                    }
                }
            }

            Console.WriteLine($"Creating a new project in {_baseFolder.Name}.\n");
            CreateFolders();

            if (_status is Status.Success)
                Helper.SuccessMessage("Project created successfully.");
            else
                Helper.ErrorMessage("An error occured. Something might be on Fire");
        }

        private void CreateFolders()
        {
            _status = _baseFolder.Create();
            _status = _config.File.Create();

            foreach (var folder in _baseFolder.Folders)
            {
                _status = folder.Create();
                if (_status is Status.Error)
                    break;
                CreateFiles(folder);
            }
        }
            // Load Folders
            // Load Files        private void CreateFiles(Folder folder)
        {
            foreach (var file in folder.Files)
            {
                _status = file.Create();
                if (_status is Status.Error)
                    break;
            }
        }

        public void Build()
        {
            if (IsHaikuProject())
            {
                Console.WriteLine($"Building project in {_baseFolder.Name}.\n");
                LoadFiles();

            }
            else
            {
                Helper.ErrorMessage("Aborting. Folder is not a valid Haiku project.");
            }
            // Process Markdown
            // copy static files
            // build html files
        }

        private void LoadFiles()
        {
            Console.Write("- Loading project files: ");
            _baseFolder.ListFolders();
            _baseFolder.ListFiles();
            foreach (var folder in _baseFolder.Folders)
            {
                folder.ListFiles();
            }
            Helper.Success();
        }
    }
}