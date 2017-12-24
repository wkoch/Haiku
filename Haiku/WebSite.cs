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

        private void UpdateName()
        {
            if (Program.CLI.OptionWasGiven())
                Name = Program.CLI.Option;
        }

        public void New()
        {
            UpdateName();
            System.Console.WriteLine($"New in {Name}");
            WriteResources();
            
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
                    var file = fileMatch.Match(resourceName).Groups[1];

                    var folderPattern = @"Haiku\.Resources\.(\w+)\.((\d*-\d*-\d*-)?\w+\.\w+)$";
                    var folderMatch = new Regex(folderPattern, RegexOptions.IgnoreCase);
                    var folder = folderMatch.Match(resourceName).Groups[1];

                    // System.Console.WriteLine($"{folder}/{file}");
                    var reader = new StreamReader(stream);

                    // System.Console.WriteLine(reader.ReadToEnd());
                }
            }
        }

        public void Build()
        {
            UpdateName();
            System.Console.WriteLine($"Build in {Name}");
        }
        // readonly private string[] _folders = { "pages", "posts", "public", "template" };
        // private (string folder, string file)[] _resources =
        // {
        //     // Samples for Pages and Posts
        //     (folder: "pages", file: "about.md"),
        //     (folder: "posts", file: "2017-12-20-hello_world.md"),

        //     // Template layout files
        //     (folder: "template", file: "layout.html"),
        //     (folder: "template", file: "_footer.html"),
        //     (folder: "template", file: "_header.html"),
        //     (folder: "template", file: "_menu.html"),
        //     (folder: "template", file: "_menu_item.html"),

        //     // Template for Blog and its partials
        //     (folder: "template", file: "_blog.html"),
        //     (folder: "template", file: "_blog_post.html"),

        //     // Template for Pages and Posts
        //     (folder: "template", file: "_page.html"),
        //     (folder: "template", file: "_post.html"),

        // };
        // private Config _config;
        // private Status _status = Status.Nothing;

        // private bool IsHaikuProject() => (_baseFolder.Exists()); //&& _config.File.Exists());


        // public WebSite()
        // {
        //     // _baseFolder = new Folder(null, baseFolderName);
        //     // _config = new Config(_baseFolder, Program.Name, "conf");
        // }

        // private void GetBaseFolder()
        // {
        //     // DefaultBaseFolder = CLI.Option is null ? 
        // }

        // public void New()
        // {
        //     foreach (var _folder in _folders)
        //     {
        //         var dir = new Folder(_baseFolder, _folder);
        //         _baseFolder.Folders.Add(dir);
        //         foreach (var resource in _resources)
        //         {
        //             if (_folder == resource.folder)
        //             {
        //                 dir.Files.Add(File.FromResource(new File(dir, resource.file)));
        //             }
        //         }
        //     }

        //     Console.WriteLine($"Creating a new project in {_baseFolder.Name}.\n");
        //     CreateFolders();

        //     if (_status is Status.Success)
        //         Helper.SuccessMessage("Project created successfully.");
        //     else
        //         Helper.ErrorMessage("An error occured. Something might be on Fire");
        // }

        // private void CreateFolders()
        // {
        //     _status = _baseFolder.Create();
        //     _status = _config.File.Create();

        //     foreach (var folder in _baseFolder.Folders)
        //     {
        //         _status = folder.Create();
        //         if (_status is Status.Error)
        //             break;
        //         CreateFiles(folder);
        //     }
        // }

        // private void CreateFiles(Folder folder)
        // {
        //     foreach (var file in folder.Files)
        //     {
        //         _status = file.Create();
        //         if (_status is Status.Error)
        //             break;
        //     }
        // }

        // public void Build()
        // {
        //     if (IsHaikuProject())
        //     {
        //         Console.WriteLine($"Building project in {_baseFolder.Name}.\n");
        //         LoadFiles();
        //         // makeHTML("pages");
        //         PrepareLayout();
        //         makeHTML("posts");
        //     }
        //     else
        //     {
        //         Helper.ErrorMessage("Aborting. Folder is not a valid Haiku project.");
        //     }
        //     // copy static files
        //     // build html files
        // }

        // private void LoadFiles()
        // {
        //     Console.Write("- Reading project files: ");
        //     _baseFolder.ListFolders();
        //     _baseFolder.ListFiles();
        //     foreach (var folder in _baseFolder.Folders)
        //     {
        //         folder.ListFiles();
        //     }
        //     Helper.Success();
        // }

        // private void makeHTML(string name)
        // {
        //     // var folder = _baseFolder.FindFolder(name);
        //     // var file = _baseFolder.FindFile("about");
        //     // if (folder is null || file is null)
        //     // {
        //     //     System.Console.WriteLine($"Error! {folder} {file}");
        //     // }
        //     // else
        //     // {
        //     //     // System.Console.WriteLine(folder.Name);
        //     //     var layout = _baseFolder.FindFile("layout");
        //     //     var header = _baseFolder.FindFile("_header");
        //     //     var footer = _baseFolder.FindFile("_footer");
        //     //     var page = _baseFolder.FindFile("_page");
        //     //     header.Contents = header.Contents.Replace("@Page.Title", file.Name);
        //     //     layout.Contents = layout.Contents.Replace("@Html.Partial(_header)", header.Contents);
        //     //     layout.Contents = layout.Contents.Replace("@Html.Partial(_footer)", footer.Contents);
        //     //     layout.Contents = layout.Contents.Replace("@Html.Render(Content)", file.Contents);
        //     //     layout.SaveAs($"{WebSite.DefaultBaseFolder}/public/about.html");
        //     // }
        //     var folder = _baseFolder.FindFolder(name);

        //     var layout = _baseFolder.FindFile("layout");
        //     var header = _baseFolder.FindFile("_header");
        //     // var footer = _baseFolder.FindFile("_footer");
        //     var page = _baseFolder.FindFile("_post");
        //     var html = layout.HTML;
        //     foreach (var file in folder.Files)
        //     {
        //         var hd = header.Contents.Replace("@Page.Title", file.Name);
        //         layout.HTML = layout.HTML.Replace("@Html.Partial(_header)", hd);
        //         layout.HTML = layout.HTML.Replace("@Html.Render(Content)", file.Markdown);
        //         var pub = _baseFolder.FindFolder("public");
        //         layout.Publish($"{pub.RelativePath}/{file.Name}.html");
        //     }

        // }

        // private bool IsBlogIndex()
        // {
        //     return (_baseFolder.FindFile("index") is null);
        // }

        // private void PrepareLayout()
        // {
        //     var layout = _baseFolder.FindFile("layout");
        //     PrepareMenu();
        //     var menu = _baseFolder.FindFile("_menu");
        //     var header = _baseFolder.FindFile("_header");
        //     var footer = _baseFolder.FindFile("_footer");
        //     footer.PrepareHTML();

        //     layout.HTML = layout.Contents.Replace("@Html.Partial(_menu)", menu.HTML);
        //     layout.HTML = layout.Contents.Replace("@Html.Partial(_footer)", footer.HTML);
        // }

        // private void PrepareMenu()
        // {
        //     string itens = "";
        //     string item = "";
        //     var menu_template = _baseFolder.FindFile("_menu");
        //     var item_template = _baseFolder.FindFile("_menu_item");
        //     var pages = _baseFolder.FindFolder("pages").Files;
        //     foreach (var page in pages)
        //     {
        //         item = item_template.Contents.Replace("@Page.Link", $"{page.Name.ToLower()}.html").Replace("@Page.Name", page.Name);
        //         itens += item;
        //     }
        //     menu_template.HTML = menu_template.Contents.Replace("@HTML.Partial(_menu_item)", itens);
        // }

        // private void BuildIndex()
        // {
        //     // Build index
        // }

        // private void BuildBlog()
        // {
        //     if (IsBlogIndex())
        //     {
        //         // Build Blog as index
        //     }
        //     else
        //     {
        //         BuildIndex();
        //         // Build Blog as /blog
        //     }
        // }

        // private void BuildPost()
        // {

        // }

        // private void BuildPage()
        // {

        // }

        // private void BuildArchive()
        // {

        // }
    }
}