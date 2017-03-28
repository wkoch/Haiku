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
            Option = (args.Length > 1) ? args[1] : null;

            if (Argument != null)
            {
                foreach (Command command in Commands)
                    if (command.Name.ToLower() == Argument.ToLower())
                        command.Execute();
            }
            else
            {
                Default.Execute();
            }
        }

        public void DefaultCommand(string name, string description, Action method)
        {
            Default = SetCommand(name, description, method);
        }

        public Command SetCommand(string name, string description, Action method)
        {
            Command command = new Command(name, description, method);
            Commands.Add(command);
            return command;
        }


    }
}