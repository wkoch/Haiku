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
            cli.DefaultCommand("Help", "Prints this text.", Help);

            cli.Parse(args);
            Console.ReadKey();

            void Help()
            {
                Console.Clear();
                Console.WriteLine("Haiku v0.1\n");
                Console.WriteLine("[Command] [Option] [Description]\n");
                foreach (var cmd in cli.Commands)
                {
                    var option = String.Equals(cmd.Name, "Help") ? "" : "Path";
                    Console.WriteLine("{0,-9} {1,-8} {2}", cmd.Name, option, cmd.Description);
                }
            }
        }


    }
}
