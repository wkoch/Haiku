using System;
using Directory = System.IO.Directory;
using System.Linq;
using System.Collections.Generic;

namespace Haiku
{
    public class Folder
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public List<Folder> Folders = new List<Folder>();
        public List<File> Files = new List<File>();
        public Status _status = Status.Nothing;


        public Folder(string path, string name)
        {
            Path = path;
            Name = name;
            FullPath = System.IO.Path.Combine(Path, Name);
        }


        public void Exists()
        {
            Directory.Exists(FullPath);
        }


        public Status Create()
        {
            CLI.BlueText();
            Console.Write("Creating ");
            CLI.CyanText();
            Console.Write($"{FullPath}: ");
            try
            {
                Directory.CreateDirectory(FullPath);
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
            Directory.Delete(FullPath, true);
        }


        public void ListFolders()
        {
            var folders = Directory.GetDirectories(FullPath);
            foreach (var folder in folders)
            {
                var folderPath = System.IO.Path.GetDirectoryName(folder);
                var folderName = System.IO.Path.GetFileName(folder);
                Files.Add(new File { Path = folderPath, Name = folderName });
            }
        }


        public void ListFiles()
        {
            var files = Directory.GetFiles(FullPath);
            foreach (var file in files)
            {
                var filepath = System.IO.Path.GetDirectoryName(file);
                var filename = System.IO.Path.GetFileName(file);
                Files.Add(new File { Path = filepath, Name = filename });
            }
        }
    }
}