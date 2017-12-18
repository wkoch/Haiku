using System;
using System.Linq;
using System.Collections.Generic;

namespace Haiku
{
    public class CLI
    {
        public static CLI obj { get; set; }
        public static List<Command> Commands = new List<Command>();
        public static string Argument { get; set; }
        public static string Option { get; set; }
        private Command _default { get; set; }
        private static readonly ConsoleColor _defaultConsoleColor = Console.ForegroundColor;


        public void Run(string[] args)
        {
            Parse(args);
            var command = FindCommand(Argument);
            if (command != null)
            {
                command.Execute();
            }
            else
            {
                _default.Execute();
            }
        }

        private static void Parse(string[] args)
        {
            Argument = (args.Length > 0) ? args[0] : null;
            Option = (args.Length > 1) ? args[1] : null;
        }

        public Command DefaultCommand(Command command)
        {
            _default = SetCommand(command);
            return command;
        }


        public Command SetCommand(Command command)
        {
            Commands.Add(command);
            return command;
        }

        public void CreateCommand(string name, string description, string argument, Action method)
        {
            var cmd = new Command(name, description, argument, method);
            Commands.Add(cmd);
        }


        public static Command FindCommand(string arg)
        {
            var getCommand = from cmd in Commands
                             where cmd.Name == arg
                             select cmd;
            return getCommand.FirstOrDefault();
        }


        public static bool OptionWasGiven()
        {
            return Option != null;
        }


        public static string GetOption()
        {
            return Option;
        }

        public static bool OptionIsCommand(string arg)
        {
            return FindCommand(arg) != null && arg != "help";
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