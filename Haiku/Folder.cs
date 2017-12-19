using System;
using Directory = System.IO.Directory;
using System.Linq;
using System.Collections.Generic;

namespace Haiku
{
    public class Folder
    {
        public Folder Parent { get; set; }
        public string Name { get; set; }
        public string RelativePath { get; set; }
        // public string FullPath { get; set; }
        public List<Folder> Folders = new List<Folder>();
        public List<File> Files = new List<File>();
        public Status _status = Status.Nothing;


        public Folder FindFolder(string name)
        {
            var getFolder = from folder in Folders
                             where folder.Name == name
                             select folder;
            return getFolder.FirstOrDefault();
        }


        public File FindFile(string name)
        {
            var getFile = from folder in Folders
                          from file in folder.Files
                          where file.Name == name
                          select file;
            return getFile.FirstOrDefault();
        }


        public Folder(Folder parent, string name)
        {
            if (parent is null)
            {
                Parent = this;
                RelativePath = name;
            }
            else
            {
                Parent = parent;
                RelativePath = System.IO.Path.Combine(Parent.RelativePath, name);
            }
            Name = name;
        }


        public bool Exists()
        {
            return Directory.Exists(RelativePath);
        }


        public Status Create()
        {
            CLI.BlueText();
            Console.Write("Creating ");
            CLI.CyanText();
            Console.Write($"{RelativePath}: ");
            try
            {
                Directory.CreateDirectory(RelativePath);
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
            Directory.Delete(RelativePath, true);
        }


        public void ListFolders()
        {
            var folders = Directory.GetDirectories(RelativePath);
            foreach (var folder in folders)
            {
                var folderPath = System.IO.Path.GetDirectoryName(folder);
                var folderName = System.IO.Path.GetFileName(folder);
                Folders.Add(new Folder(this, folderName));
            }
        }


        public void ListFiles()
        {
            var files = Directory.GetFiles(RelativePath);
            foreach (var file in files)
            {
                var filepath = System.IO.Path.GetDirectoryName(file);
                var filename = System.IO.Path.GetFileName(file);
                var fileObj = new File(this, filename);
                Files.Add(fileObj);
                fileObj.ReadContents();
            }
        }
    }
}