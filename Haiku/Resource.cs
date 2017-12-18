using System;
using System.IO;
using System.Resources;
using System.Reflection;


namespace Haiku
{
    public class Resource
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FullPath { get; set; }
        public string[] Contents { get; set; }
        public Status _status = Status.Nothing;


        public Resource(string path, string name)
        {
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(name);
            System.Console.WriteLine(Name);
            Extension = System.IO.Path.GetExtension(name);
            System.Console.WriteLine(Extension);
            FullPath = System.IO.Path.Combine(Path, name);
            // File = new File(path, $"{name}.{extension}");
        }

        public static File FileFromResource(string path, string filename)
        {
            var file = new File(path, filename);
            var assembly = typeof(Haiku.Program).GetTypeInfo().Assembly;
            var resourceName = $"Haiku.Resources.{path}.{filename}";
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            StreamReader reader = new StreamReader(stream);
            file.Contents = reader.ReadToEnd();
            return file;
        }


        
    }
}