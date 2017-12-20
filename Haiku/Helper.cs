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
            CLI.RedText();
            Console.WriteLine("Error!");
            CLI.DefaultColor();
        }


        public static void Success()
        {
            CLI.GreenText();
            Console.WriteLine("OK");
            CLI.DefaultColor();
        }

        public static void ErrorMessage(string msg)
        {
            CLI.RedText();
            System.Console.WriteLine($"\n{msg}.");
            CLI.DefaultColor();
        }

        public static void SuccessMessage(string msg)
        {
            CLI.GreenText();
            System.Console.WriteLine($"\n{msg}");
            CLI.DefaultColor();
        }
    }
}