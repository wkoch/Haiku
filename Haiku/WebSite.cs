using System;
using System.IO;
using System.Collections.Generic;

namespace Haiku
{
    public class WebSite
    {
        private Folder _baseFolder;
        readonly private string[] _folders = { "pages", "posts", "public", "template" };
        readonly private Config _config = new Config { Name = Program.AppName, Extension = "conf" };
        private Status _status = Status.Nothing;


        public WebSite(string folder)
        {
            var path = (folder != null) ? folder : "HaikuWebsite";
            _baseFolder = new Folder("", path);
            foreach (var _folder in _folders)
            {
                _baseFolder.Folders.Add(new Folder(_baseFolder.FullPath, _folder));
            }
        }

        public void New()
        {
            Console.WriteLine($"Creating a new project in {_baseFolder.Name}.\n");
            CreateFolders();
            // create files
            if (_status is Status.Success)
            {
                Helper.SuccessMessage("Project created successfully.");}
        }

        private void CreateFolders()
        {
            _status = _baseFolder.Create();

            foreach (var folder in _baseFolder.Folders)
            {
                _status = folder.Create();
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