using System;
using System.Linq;
using System.Collections.Generic;

namespace Haiku
{
    public class CLI
    {
        private static List<Command> Commands = new List<Command>();
        private static string _argument { get; set; }
        private static string _option { get; set; }
        private static Command _help { get; set; }
        private static readonly ConsoleColor _defaultConsoleColor = Console.ForegroundColor;


        public void Run(string[] args)
        {
            _help = SetCommand(new Command
            {
                Name = "help",
                Description = " Prints this text, or any given command's Help.",
                Help = "Seriously?",
                Method = CLI.Help
            });

            Parse(args);
            var command = FindCommandOrHelp(_argument);
            command?.Execute();
        }


        private static void Parse(string[] args)
        {
            _argument = (args.Length > 0) ? args[0] : null;
            _option = (args.Length > 1) ? args[1] : null;
        }


        public Command SetCommand(Command command)
        {
            Commands.Add(command);
            return command;
        }


        public static Command FindCommand(string arg)
        {
            var getCommand = from cmd in Commands
                             where cmd.Name == arg
                             select cmd;
            return getCommand.FirstOrDefault();
        }


        private static Command FindCommandOrHelp(string arg)
        {
            var command = FindCommand(arg);
            if (command != null)
                return command;
            else
                return FindCommand("help");
        }


        public static bool OptionWasGiven()
        {
            return _option != null;
        }


        public static string GetOption()
        {
            return _option;
        }


        public static bool OptionIsCommand(string arg)
        {
            return FindCommand(arg) != null;
        }


        public static void Help()
        {
            Console.WriteLine($"{Program.AppName} v{Program.AppVersion}\n");
            if (OptionWasGiven() && OptionIsCommand(_option))
            {
                var cmd = FindCommand(_option);
                Console.WriteLine($"Usage: {Program.AppName.ToLower()} {cmd.Name} [argument]");
                if (cmd.Argument != null)
                {
                    Console.WriteLine("\nArguments:");
                    Console.WriteLine($"  [{cmd.Argument}]  {cmd.Help}");
                }
            }
            else
            {
                Console.WriteLine($"Usage: {Program.AppName.ToLower()} [command] [option]\n");
                Console.WriteLine("Commands:");
                foreach (var command in Commands)
                {
                    Console.WriteLine($"  {command.Name}: {command.Description}");
                }
                Console.WriteLine($"\nUse \"{Program.AppName.ToLower()} help [command]\" for more information about a command.");
            }
        }


        public static void SetColor(ConsoleColor color) => Console.ForegroundColor = color;
        public static void DefaultColor() => SetColor(_defaultConsoleColor);
        public static void BlueText() => SetColor(ConsoleColor.Blue);
        public static void CyanText() => SetColor(ConsoleColor.Cyan);
        public static void GreenText() => SetColor(ConsoleColor.Green);
        public static void RedText() => SetColor(ConsoleColor.Red);
        public static void GrayText() => SetColor(ConsoleColor.Gray);
    }
}