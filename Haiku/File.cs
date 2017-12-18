using System;

namespace Haiku
{
    public class File
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FullPath { get; set; }
        public string Contents { get; set; }
        public Folder Parent { get; set; }
        public Status _status = Status.Nothing;


        public File(string path, string name)
        {
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(name);
            // System.Console.WriteLine(Name);
            // Extension = System.IO.Path.GetExtension(name);
            // System.Console.WriteLine(Extension);
            FullPath = System.IO.Path.Combine(Path, name);
        }


        public Status Create()
        {
            CLI.BlueText();
            Console.Write("Creating ");
            CLI.CyanText();
            Console.Write($"{FullPath}: ");
            try
            {
                if (Contents != null)
                    System.IO.File.WriteAllText(FullPath, Contents);
                else
                    System.IO.File.Create(FullPath);
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
    }
}