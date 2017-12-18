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
            (folder: "template", file: "_page.ghtml"),
            (folder: "template", file: "layout.html"),
        };
        readonly private Config _config;
        private Status _status = Status.Nothing;


        public WebSite(string folder)
        {
            var path = (folder != null) ? folder : "HaikuWebsite";
            _baseFolder = new Folder("", path);
            _config = new Config(_baseFolder.FullPath, Program.AppName, "conf");
            foreach (var _folder in _folders)
            {
                var dir = new Folder(_baseFolder.FullPath, _folder);
                _baseFolder.Folders.Add(dir);
                foreach (var resource in _resources)
                {
                    if (_folder == resource.folder)
                    {
                        dir.Files.Add(Resource.FileFromResource(resource.folder, resource.file));
                    }
                }
            }
        }

        public void New()
        {
            Console.WriteLine($"Creating a new project in {_baseFolder.Name}.\n");
            CreateFolders();
            

            if (_status is Status.Success)
            {
                Helper.SuccessMessage("Project created successfully.");
            }
        }

        private void CreateFolders()
        {
            _status = _baseFolder.Create();
            _status = _config.File.Create();

            foreach (var folder in _baseFolder.Folders)
            {
                _status = folder.Create();
                if (_status is Status.Error)
                {
                    Helper.ErrorMessage();
                    break;
                }
                CreateFiles(folder);
            }
        }

        private void CreateFiles(Folder folder)
        {
            foreach (var file in folder.Files)
            {
                _status = file.Create();
                if (_status is Status.Error)
                {
                    Helper.ErrorMessage();
                    break;
                }
            }
        }

        public void Build()
        {
            // check existing project
            // read config
            // load template
            // load content
            // copy static files
            // build html files
        }
    }
}