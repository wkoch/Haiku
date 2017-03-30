using System;

namespace Haiku
{
    class Program
    {
        static void Main(string[] args)
        {
            Cli cli = new Cli();
            Haiku project = new Haiku();
            if (args.Length > 1)
                project.BaseDir = args[1];

            cli.SetCommand(new Command("New", "Create a new project.", project.New));
            cli.SetCommand(new Command("Build", "Builds the project.", project.Build));
            cli.DefaultCommand(new Command("Help", "Prints the Help text.", Help));

            cli.Parse(args);
            Console.ReadKey();
        }

        public static void Help()
        {
            Console.Clear();
            Console.WriteLine("\nHaiku v0.1\t03/2017\n");
            Console.WriteLine("\tHaiku [command] [option]\n");
            Console.WriteLine("\tnew path\t Creates a new project. Optional path.\n");
            Console.WriteLine("\tbuild path\t Builds the project on current/specified folder.\n");
            Console.WriteLine("\thelp\t\t Prints this text.\n");
        }
    }
}
