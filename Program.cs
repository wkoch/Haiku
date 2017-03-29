using System;

namespace Haiku
{
    class Program
    {
        static void Main(string[] args)
        {
            Cli cli = new Cli();

            cli.SetCommand(new Command("New", "Create a new project.", New));
            cli.SetCommand(new Command("Build", "Builds the project.", Build));
            cli.DefaultCommand(new Command("Help", "Prints the Help text.", Help));

            cli.Parse(args);
        }

        public static void New()
        {
            Console.WriteLine("Creating a new project.");
        }

        public static void Build()
        {
            Console.WriteLine("Building this project.");
        }

        public static void Help()
        {
            Console.WriteLine("Help Text.");
        }
    }
}
