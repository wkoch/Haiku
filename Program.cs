using System;
// using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Haiku
{
    class Program
    {
        public static readonly string Name = "Haiku";
        public static readonly string Version = "0.9.0";
        public static CLI CLI = new CLI();
        public static WebSite Project = new WebSite();


        static void Main(string[] args)
        {
            CLI.SetCommand(new Command
            {
                Name = "new",
                Description = "  Creates a new project. Optional folder.",
                Argument = "folder",
                Help = "Path for creating a new project.",
                Method = Project.New
            });

            CLI.SetCommand(new Command
            {
                Name = "build",
                Description = "Builds an existing project. Optional folder.",
                Argument = "folder",
                Help = "Path to an existing project.",
                Method = Project.Build
            });

            CLI.Run(args);
        }
    }
}