using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Haiku
{
    class Program
    {
        public static string AppName = "Haiku";
        public static string AppVersion = "0.1.0";
        static void Main(string[] args)
        {
            Cli cli = new Cli();

            cli.SetCommand(new Command("New", "Creates a new project, optional directory.", New));
            cli.SetCommand(new Command("Build", "Builds existing project, optional directory", Build));
            cli.DefaultCommand(new Command("Help", "Prints this Help text.", Help));

            cli.Parse(args);

            // var haiku = new WebSite();
        }

        

        public static void New()
        {
            if (Cli.Option != null)
                Console.WriteLine($"Creating a new project in {Cli.Option}.");
            else
                Console.WriteLine("Creating a new project.");
        }

        public static void Build()
        {
            Console.WriteLine("Building this project.");
        }

        public static void Help()
        {
            Console.WriteLine($"{AppName} v{AppVersion}\n");
            System.Console.WriteLine("Usage:\n  haiku [Command] [Option]\n");
            System.Console.WriteLine("Commands:");
            foreach (var command in Cli.Commands)
            {
                System.Console.WriteLine($"  {command.Name}: {command.Description}");
            }
        }
    }
}