using System;

namespace Haiku
{
    public class Command
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Action Method { get; set; }
        public string Option { get; set; }

        public Command(string name, string description, Action method)
        {
            Name = name;
            Description = description;
            Method = method;
        }
        public void Execute()
        {
            Method();
        }
    }
}