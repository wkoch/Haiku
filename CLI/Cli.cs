using System;

namespace Haiku
{
    public class Cli
    {
        private string Argument { get; set; }

        public void Parse(string[] args)
        {
            try
            {
                Argument = args[0].ToLower();
                foreach (var command in Command.List)
                    if (string.Equals(command.Name.ToLower(), Argument))
                        command.Execute();
            }
            catch (System.Exception)
            {
                Command.Default.Execute();
            }
        }

        public Command SetCommand(string name, string description, Action method)
        {
            Command command = new Command(name, description, method);
            Command.List.Add(command);
            return command;
        }
    }
}