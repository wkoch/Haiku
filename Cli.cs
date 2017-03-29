using System;
using System.Collections.Generic;

namespace Haiku
{
    public class Cli
    {
        public List<Command> Commands = new List<Command>();
        private string Argument { get; set; }
        private string Option { get; set; }
        private Command Default { get; set; }

        public void Parse(string[] args)
        {
            Argument = (args.Length > 0) ? args[0] : null;

            if (Argument != null)
            {
                foreach (Command command in Commands)
                    if (command.Name.ToLower() == Argument.ToLower())
                    {
                        command.Option = (args.Length > 1) ? args[1] : null;
                        command.Execute();
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