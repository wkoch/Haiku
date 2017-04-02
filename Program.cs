using System;

namespace Haiku
{
    class Program
    {
        static void Main(string[] args)
        {
            var cli = new Cli();
            var project = new Haiku();

            if (args.Length > 1)
                project.BaseDir = args[1];

            cli.SetCommand("New", "Creates a new project. Optional path.", project.New);
            cli.SetCommand("Build", "Builds the project on current/specified folder.", project.Build);
            Command.Default = cli.SetCommand("Help", "Prints this text.", Help);

            cli.Parse(args);
            Console.ReadKey();
        }

        static void Help()
        {
            Console.Clear();
            Console.WriteLine("Haiku v0.1\n");
            Console.WriteLine("[Command] [Option] [Description]\n");
            foreach (var cmd in Command.List)
            {
                var option = String.Equals(cmd.Name, "Help") ? "" : "Path";
                Console.WriteLine($"{cmd.Name,-9} {option,-8} {cmd.Description}");
            }
        }
    }
}
