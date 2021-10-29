using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptek.Util
{
    static class Helper
    {
        public static void PrintLine(object obj, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(obj);
            Console.ResetColor();
        }

        public static void Print(object obj, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(obj);
            Console.ResetColor();
        }

        public static void IncorrectMessage()
        {
            Helper.PrintLine($"Incorrect. Please try again", ConsoleColor.Red);
        }
    }
}
