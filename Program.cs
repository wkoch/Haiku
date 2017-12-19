using System;
// using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Haiku
{
    class Program
    {
        public static readonly string AppName = "Haiku";
        public static readonly string AppVersion = "0.9.0";


        static void Main(string[] args)
        {
            var cli = new CLI();

            cli.SetCommand(new Command
            {
                Name = "new",
                Description = "  Creates a new project. Optional folder.",
                Argument = "folder",
                Help = "Path for creating a new project.",
                Method = New
            });

            cli.SetCommand(new Command
            {
                Name = "build",
                Description = "Builds an existing project. Optional folder.",
                Argument = "folder",
                Help = "Path to an existing project.",
                Method = Build
            });

            cli.Run(args);
        }


        public static void New()
        {
            var haiku = new WebSite(CLI.GetOption());
            haiku.New();
        }


        public static void Build()
        {
            var haiku = new WebSite(CLI.GetOption());
            haiku.Build();
        }
    }
}