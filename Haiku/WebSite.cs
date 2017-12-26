using System;
using System.IO;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
        public Template Layout;

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
                    File.WriteAllText(Path.Combine(BaseFolder.Name, folder, file), reader.ReadToEnd());
                }
            }
        }

        public void Build()
        {
            UpdateName();
            System.Console.WriteLine($"Building \"{BaseFolder.Name}\".");
            MakePublicFolder();
            CopyCSS();
            ReadFolders();
            ReadFiles();
            BuildProject();
        }

        private void MakePublicFolder()
        {
            var path = Path.Combine(BaseFolder.Name, "public");
            CleanPublicFolder(path);
            Directory.CreateDirectory(path);
        }

        private void CleanPublicFolder(string path)
        {
            if (Directory.Exists(path))
            {
                var dirs = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);
                if (dirs.Length is 0 && files.Length is 0)
                    Directory.Delete(path);
                else
                {
                    foreach (var file in files)
                    {
                        File.Delete(file);
                    }
                }
            }
        }

        private void CopyCSS()
        {
            var origin = Path.Combine(BaseFolder.Name, "template");
            var destination = Path.Combine(BaseFolder.Name, "public");
            var cssFiles = Directory.GetFiles(origin, "*.css", SearchOption.TopDirectoryOnly);
            foreach (var file in cssFiles)
            {
                var path = Path.Combine(destination,Path.GetFileName(file));
                File.Copy(file, path);
            }
        }

        private void ReadFiles()
        {
            foreach (var folder in Folders)
            {
                var files = ListFiles(folder.Path);
                foreach (var filepath in files)
                {
                    var file = Path.GetFileName(filepath);
                    switch (folder.Name)
                    {
                        case "pages":
                            var page = new Page { Name = file, Path = filepath };
                            Pages.Add(page);
                            page.Process();
                            // System.Console.WriteLine($"File: {file}, Path: {filepath}");
                            break;
                        case "posts":
                            var post = new Post { Name = file, Path = filepath };
                            Posts.Add(post);
                            post.Process();
                            // System.Console.WriteLine($"File: {file}, Path: {filepath}");
                            break;
                        case "template":
                            if (file == "layout.html")
                            {
                                Layout = new Template { Name = file, Path = filepath };
                                Layout.Process();
                            }
                            else
                            {
                                var template = new Template { Name = file, Path = filepath };
                                Templates.Add(template);
                                template.Process();
                            }
                            // System.Console.WriteLine($"File: {file}, Path: {filepath}");
                            break;
                        default:
                            break;
                    }
                }
            }
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
            PreparePages();
            PrepareMenu();
            PrepareLayout();
            // BuildIndex();
            BuildPages();
            // BuildPosts();
            // CopyStaticFiles();
        }

        private void PrepareLayout()
        {
            foreach (var template in Templates)
            {
                Layout.Content = Layout.Content.Replace($"@Html.Partial({Path.GetFileNameWithoutExtension(template.Name)})", template.Content);
                Layout.Content = Layout.Content.Replace("@WebSite.Title", Name);
            }
        }

        private void PreparePages()
        {
            foreach (var page in Pages)
            {
                page.HTML = new HTML { Path = Path.Combine(BaseFolder.Name, "public", Path.ChangeExtension(page.Name, "html")), Slug = Path.ChangeExtension(page.Name, "html") };
            }
        }

        private void PrepareMenu()
        {
            var menu = FindTemplate("_menu.html");
            var menuItem = FindTemplate("_menu_item.html");
            string tmp = "";

            foreach (var page in Pages)
            {
                var item = menuItem.Content.Replace("@Page.Link", page.HTML.Slug);
                tmp += item.Replace("@Page.Name", page.Title);
            }
            menu.Content = menu.Content.Replace("@HTML.Partial.Each(_menu_item)", tmp);
            Layout.Content = Layout.Content.Replace($"@Html.Partial(_menu)", menu.Content);
        }

        private void BuildPages()
        {
            foreach (var page in Pages)
            {
                // page.HTML = new HTML { Path = Path.Combine("public", Path.ChangeExtension(page.Name, "html")), Slug = Path.ChangeExtension(page.Name, "html") };
                var template = FindTemplate("_page.html");
                page.HTML.Export = Layout.Content.Replace("@Html.Render(Content)", template.Content);
                page.HTML.Export = page.HTML.Export.Replace("@Page.Markdown", page.Markdown);
                page.HTML.Export = page.HTML.Export.Replace("@Page.Title", page.Title);
                if (page.SubTitle is null)
                    page.HTML.Export = page.HTML.Export.Replace("@Page.SubTitle", "");
                else
                    page.HTML.Export = page.HTML.Export.Replace("@Page.SubTitle", page.SubTitle);
                File.WriteAllText(page.HTML.Path, page.HTML.Export);
            }
        }

        private Template FindTemplate(string name)
        {
            foreach (var template in Templates)
            {
                if (template.Name == name)
                    return template;
            }
            return null;
        }
    }
}