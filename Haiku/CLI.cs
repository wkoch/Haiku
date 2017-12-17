using System;
using System.Linq;
using System.Collections.Generic;

namespace Haiku
{
    public class Cli
    {
        public static List<Command> Commands = new List<Command>();
        private string Argument { get; set; }
        public static string Option { get; set; }
        private Command Default { get; set; }

        public void Parse(string[] args)
        {
            string Argument = (args.Length > 0) ? args[0] : null;

            if (Argument != null)
            {
                var getCommand =
                    from cmd in Commands
                    where cmd.Name.ToLower() == Argument.ToLower()
                    select cmd;
                Command command = getCommand.FirstOrDefault();
                if (command != null)
                {
                    Cli.Option = (args.Length > 1) ? args[1] : null;
                    command.Execute();
                }
                else
                {
                    Default.Execute();
                }
            }
            else
            {
                Default.Execute();
            }
        }

        public Command DefaultCommand(Command command)
        {
            Default = SetCommand(command);
            return command;
        }

        public Command SetCommand(Command command)
        {
            Commands.Add(command);
            return command;
        }


    }
}