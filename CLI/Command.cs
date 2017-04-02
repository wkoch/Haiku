using System;
using System.Collections.Generic;

namespace Haiku
{
    public class Command
    {
        // Class variables
        public static List<Command> List = new List<Command>();
        public static Command Default { get; set; }
        // Instance variables
        public string Name { get; set; }
        public string Description { get; set; }
        public Action Method { get; set; }

        public Command(string name, string description, Action method)
        {
            Name = name;
            Description = description;
            Method = method;
        }
        public void Execute() => Method();
    }
}