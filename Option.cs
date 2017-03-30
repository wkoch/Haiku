using System;

namespace Haiku
{
    public class Option
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Option(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}