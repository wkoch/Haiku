using System;
using System.Resources;
using System.Reflection;
using System.Collections;
using HeyRed.MarkdownSharp;
using Stream = System.IO.Stream;
using StringReader = System.IO.StringReader;
using StreamReader = System.IO.StreamReader;

namespace Haiku
{
    public class File
    {
        public Folder Parent { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }
        public string Contents { get; set; }
        public string Title { get; set; }
        public string Markdown { get; set; }
        public string HTML { get; set; }
        public Status _status = Status.Nothing;


        public File(Folder folder, string filename)
        {
            Name = System.IO.Path.GetFileNameWithoutExtension(filename);
            Extension = System.IO.Path.GetExtension(filename);
            Parent = folder;
            FilePath = System.IO.Path.Combine(folder.RelativePath, filename);
        }


        public bool Exists()
        {
            return System.IO.File.Exists(FilePath);
        }


        public void ReadContents()
        {
            Contents = System.IO.File.ReadAllText(FilePath);
            if (Extension is ".md")
                ProcessMarkdown();
        }


        public Status Create()
        {
            Program.CLI.BlueText();
            Console.Write("Creating ");
            Program.CLI.CyanText();
            Console.Write($"{FilePath}: ");
            try
            {
                if (Contents != null)
                    System.IO.File.WriteAllText(FilePath, Contents);
                else
                    System.IO.File.Create(FilePath);
            }
            catch (System.Exception)
            {
                _status = Status.Error;
            }
            finally
            {
                if (_status is Status.Error)
                {
                    Helper.Error();
                }
                else
                {
                    Helper.Success();
                    _status = Status.Success;
                }

            }
            return _status;
        }

        public Status Publish(string filepath)
        {
            Program.CLI.BlueText();
            Console.Write("Creating ");
            Program.CLI.CyanText();
            Console.Write($"{filepath}: ");
            try
            {
                if (HTML != null)
                    System.IO.File.WriteAllText(filepath, HTML);
                else
                    System.IO.File.Create(filepath);
            }
            catch (System.Exception)
            {
                _status = Status.Error;
            }
            finally
            {
                if (_status is Status.Error)
                {
                    Helper.Error();
                }
                else
                {
                    Helper.Success();
                    _status = Status.Success;
                }

            }
            return _status;
        }

        public void Delete()
        {
            System.IO.File.Delete(FilePath);
        }

        public static File FromResource(File file)
        {
            var assembly = typeof(Haiku.Program).GetTypeInfo().Assembly;
            var resourceName = "Haiku.Resources." + file.Parent.Name + "." + file.Name + file.Extension;
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            StreamReader reader = new StreamReader(stream);
            file.Title = reader.ReadLine();
            file.Contents = reader.ReadToEnd();
            return file;
        }

        public void ProcessMarkdown()
        {
            var markdown = new Markdown();
            var contents = new StringReader(Contents);
            Title = contents.ReadLine();
            Markdown = markdown.Transform(contents.ReadToEnd());
        }


        public void PrepareHTML()
        {
            HTML = Contents;
        }
    }
}