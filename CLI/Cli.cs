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
                Argument = args[0].ToLower();
                foreach (var command in Commands)
                    if (string.Equals(command.Name.ToLower(), Argument))
                        command.Execute();
            }
            catch (System.Exception)
            {
                Default.Execute();
            }
        }

        public Command DefaultCommand(string name, string description, Action method)
        {
            Default = SetCommand(name, description, method);
            return Default;
        }

        public Command SetCommand(string name, string description, Action method)
        {
            Command command = new Command(name, description, method);
            Commands.Add(command);
            return command;
        }
    }
}