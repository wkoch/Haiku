using System;

namespace Haiku
{
    public class Haiku
    {
        public static void New()
        {
            Console.WriteLine("Creating a new project.");
            string BaseDir = "HaikuWebsite";
            string[] Folders = { "pages", "posts", "public", "template" };

            foreach (string folder in Folders)
            {
                Console.WriteLine(System.IO.Path.Combine(BaseDir, folder));
                // System.IO.Directory.CreateDirectory(System.IO.Path.Combine(BaseDir, folder));
            }
        }

        public static void Build()
        {
            Console.WriteLine("Building this project.");
        }
    }
}