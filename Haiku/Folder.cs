using System;
using Directory = System.IO.Directory;
using System.Linq;
using System.Collections.Generic;

namespace Haiku
{
    public class Folder
    {
        // public Folder Parent { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public Folder(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}