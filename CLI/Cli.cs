using System;
using System.Collections.Generic;

namespace Haiku
{
    public class Cli
    {
        public List<Command> Commands = new List<Command>();
        private string Argument { get; set; }
        private Command Default { get; set; }

        public void Parse(string[] args)
        {
            try
            {
                Argument = (args.Length > 0) ? args[0] : null;
                foreach (var command in Commands)
                    if (command.Name.ToLower() == Argument.ToLower())
                        command.Execute();
            }
            catch (System.Exception)
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