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
        public static readonly string DefaultFolder = "HaikuWebsite";



        static void Main(string[] args)
        {
            CLI.obj = new CLI();
            var cli = CLI.obj;

            var NewCmd = cli.SetCommand(new Command("new", "  Creates a new project. Optional folder.", "folder",  New));
            NewCmd.Help = "Path for creating a new project.";

            var BuildCmd = cli.SetCommand(new Command("build", "Builds an existing project. Optional folder.", "folder", Build));
            BuildCmd.Help = "Path to an existing project.";

            cli.DefaultCommand(new Command("help", " Prints this text, or any given command's Help.", null, Help));

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


        public static void Help()
        {
            Console.WriteLine($"{AppName} v{AppVersion}\n");

            if (CLI.OptionWasGiven() && CLI.OptionIsCommand(CLI.Option))
            {
                var cmd = CLI.FindCommand(CLI.Option);
                Console.WriteLine($"Usage: {AppName.ToLower()} {cmd.Name} [argument]\n");
                Console.WriteLine("Arguments:");
                Console.WriteLine($"  [path]  {cmd.Help}");
            }
            else
            {
                Console.WriteLine($"Usage: {AppName.ToLower()} [command] [option]\n");
                Console.WriteLine("Commands:");
                foreach (var command in CLI.Commands)
                {
                    Console.WriteLine($"  {command.Name}: {command.Description}");
                }
                Console.WriteLine($"\nUse \"{AppName.ToLower()} help [command]\" for more information about a command.");
            }
        }
    }
}