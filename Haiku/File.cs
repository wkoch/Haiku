using System;
using Stream = System.IO.Stream;
using StreamReader = System.IO.StreamReader;
using System.Resources;
using System.Reflection;
using System.Collections;

namespace Haiku
{
    public class File
    {
        public Folder Parent { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }
        public string Contents { get; set; }
        public Status _status = Status.Nothing;


        public File(Folder folder, string filename)
        {
            // Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(filename);
            Extension = System.IO.Path.GetExtension(filename);
            Parent = folder;
            FilePath = System.IO.Path.Combine(folder.RelativePath, filename);
        }


        public bool Exists()
        {
            return System.IO.File.Exists(FilePath);
        }


        public Status Create()
        {
            CLI.BlueText();
            Console.Write("Creating ");
            CLI.CyanText();
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

        public static File FromResource(File file)
        {
            var assembly = typeof(Haiku.Program).GetTypeInfo().Assembly;
            var resourceName = "Haiku.Resources." + file.Parent.Name + "." + file.Name + file.Extension;
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            StreamReader reader = new StreamReader(stream);
            file.Contents = reader.ReadToEnd();
            return file;
        }
    }
}