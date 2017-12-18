using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Haiku
{
    class Program
    {
        public static readonly string AppName = "Haiku";
        public static readonly string AppVersion = "0.2.0";


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
            if (CLI.OptionWasGiven())
                Console.WriteLine($"Creating a new project in {CLI.GetOption()}.");
            else
                Console.WriteLine("Creating a new project.");
        }


        public static void Build()
        {
            if (CLI.OptionWasGiven())
                Console.WriteLine($"Building the project in {CLI.GetOption()}.");
            else
                Console.WriteLine("Building this project.");
        }
    }
}