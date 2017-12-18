using System;

namespace Haiku
{
    public class Command
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Argument { get; set; }
        public string Help { get; set; }
        public Action Method { get; set; }
        

        public Command(string name, string description, string argument, Action method)
        {
            Name = name.ToLower();
            Description = description;
            Argument = argument;
            Method = method;
        }


        public void Execute()
        {
            Method();
        }
    }
}