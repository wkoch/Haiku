using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Haiku
{
    public class WebSite
    {
        private Folder _baseFolder;
        readonly private string[] _folders = { "pages", "posts", "public", "template" };
        private (string folder, string file)[] _resources =
        {
            // Samples for Pages and Posts
            (folder: "pages", file: "about.md"),
            (folder: "posts", file: "2017-12-20-hello_world.md"),

            // Template layout files
            (folder: "template", file: "layout.html"),
            (folder: "template", file: "_footer.html"),
            (folder: "template", file: "_header.html"),
            (folder: "template", file: "_menu.html"),
            (folder: "template", file: "_menu_item.html"),

            // Template for Blog and its partials
            (folder: "template", file: "_blog.html"),
            (folder: "template", file: "_blog_post.html"),

            // Template for Pages and Posts
            (folder: "template", file: "_page.html"),
            (folder: "template", file: "_post.html"),
            
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

        private void CreateFiles(Folder folder)
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
                makeHTML("pages");
            }
            else
            {
                Helper.ErrorMessage("Aborting. Folder is not a valid Haiku project.");
            }
            // copy static files
            // build html files
        }

        private void LoadFiles()
        {
            Console.Write("- Reading project files: ");
            _baseFolder.ListFolders();
            _baseFolder.ListFiles();
            foreach (var folder in _baseFolder.Folders)
            {
                folder.ListFiles();
            }
            Helper.Success();
        }

        private void makeHTML(string name)
        {
            var folder = _baseFolder.FindFolder(name);
            var file = _baseFolder.FindFile("about");
            if (folder is null || file is null)
            {
                System.Console.WriteLine($"Error! {folder} {file}");
            }
            else
            {
                // System.Console.WriteLine(folder.Name);
                var layout = _baseFolder.FindFile("layout");
                var header = _baseFolder.FindFile("_header");
                var footer = _baseFolder.FindFile("_footer");
                var page = _baseFolder.FindFile("_page");
                header.Contents = header.Contents.Replace("@Page.Title", file.Name);
                layout.Contents = layout.Contents.Replace("@Html.Partial(_header)", header.Contents);
                layout.Contents = layout.Contents.Replace("@Html.Partial(_footer)", footer.Contents);
                layout.Contents = layout.Contents.Replace("@Html.Render(Content)", file.Contents);
                layout.SaveAs("HaikuWebsite/public/about.html");
            }
        }

        private void BuildIndex()
        {
            var index = _baseFolder.FindFile("index");
            if (index is null)
            {
                // Builds blog as index
            }
            else
            {
                // Builds index file as a page
                // and Blog as /blog
            }

        }

        private void BuildBlog()
        {
            
        }

        private void BuildPost()
        {
            
        }

        private void BuildPage()
        {
            
        }

        private void BuildArchive()
        {
            
        }
    }
}