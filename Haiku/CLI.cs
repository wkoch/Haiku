using System;
using System.Linq;
using System.Collections.Generic;

namespace Haiku
{
    public class CLI
    {
        private List<Command> Commands = new List<Command>();
        public string Argument { get; set; }
        public string Option { get; set; }
        private Command _help { get; set; }
        private readonly ConsoleColor _defaultConsoleColor = Console.ForegroundColor;


        public void Run(string[] args)
        {
            _help = SetCommand(new Command
            {
                Name = "help",
                Description = " Prints this text, or any given command's Help.",
                Help = "Seriously?",
                Method = Help
            });

            Parse(args);
            var command = FindCommandOrHelp(Argument);
            command?.Execute();
        }


        private void Parse(string[] args)
        {
            Argument = (args.Length > 0) ? args[0] : null;
            Option = (args.Length > 1) ? args[1] : null;
        }


        public Command SetCommand(Command command)
        {
            Commands.Add(command);
            return command;
        }


        public Command FindCommand(string arg)
        {
            var getCommand = from cmd in Commands
                             where cmd.Name == arg
                             select cmd;
            return getCommand.FirstOrDefault();
        }


        private Command FindCommandOrHelp(string arg)
        {
            var command = FindCommand(arg);
            return command ?? FindCommand("help");
        }


        public bool OptionWasGiven() => Option != null;

        public bool OptionIsCommand() => FindCommand(Option) != null;


        public void Help()
        {
            Console.WriteLine($"{Program.Name} v{Program.Version}\n");
            if (OptionWasGiven() && OptionIsCommand())
            {
                var cmd = FindCommand(Option);
                Console.WriteLine($"Usage: {Program.Name.ToLower()} {cmd.Name} [argument]");
                if (cmd.Argument != null)
                {
                    Console.WriteLine("\nArguments:");
                    Console.WriteLine($"  [{cmd.Argument}]  {cmd.Help}");
                }
            }
            else
            {
                Console.WriteLine($"Usage: {Program.Name.ToLower()} [command] [option]\n");
                Console.WriteLine("Commands:");
                foreach (var command in Commands)
                {
                    Console.WriteLine($"  {command.Name}: {command.Description}");
                }
                Console.WriteLine($"\nUse \"{Program.Name.ToLower()} help [command]\" for more information about a command.");
            }
        }


        public void SetColor(ConsoleColor color) => Console.ForegroundColor = color;
        public void DefaultColor() => SetColor(_defaultConsoleColor);
        public void BlueText() => SetColor(ConsoleColor.Blue);
        public void CyanText() => SetColor(ConsoleColor.Cyan);
        public void GreenText() => SetColor(ConsoleColor.Green);
        public void RedText() => SetColor(ConsoleColor.Red);
        public void GrayText() => SetColor(ConsoleColor.Gray);
    }
}