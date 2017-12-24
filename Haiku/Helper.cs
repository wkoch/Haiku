using System;
using System.IO;
using System.Resources;
using System.Reflection;

namespace Haiku
{
    public enum Status
    {
        Nothing, Error, Success
    };


    public static class Helper
    {
        public static void Error()
        {
            Program.CLI.RedText();
            Console.WriteLine("Error!");
            Program.CLI.DefaultColor();
        }


        public static void Success()
        {
            Program.CLI.GreenText();
            Console.WriteLine("OK");
            Program.CLI.DefaultColor();
        }

        public static void ErrorMessage(string msg)
        {
            Program.CLI.RedText();
            System.Console.WriteLine($"\n{msg}.");
            Program.CLI.DefaultColor();
        }

        public static void SuccessMessage(string msg)
        {
            Program.CLI.GreenText();
            System.Console.WriteLine($"\n{msg}");
            Program.CLI.DefaultColor();
        }
    }
}