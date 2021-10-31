using Aptek.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

        public static void PrintLine(object obj,ConsoleColor foreGround, ConsoleColor backGround)
        {
            Console.BackgroundColor = backGround;
            Console.ForegroundColor = foreGround;
            Console.WriteLine(obj);
            Console.ResetColor();
        }

        public static void PrintSlowMotion(int speed, string text, ConsoleColor foreGround)
        {
            Console.ForegroundColor = foreGround;
            for (int i = 0; i < text.Length; i++)
            {
                Thread.Sleep(speed);
                Console.Write(text[i]);
            }
            Console.ResetColor();
            Console.Write(Environment.NewLine);
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

        public static void PharmacyEmphtyMessage()
        {
            Helper.PrintLine("Pharmacy is emphty", ConsoleColor.Red);
        }

        public static void IdNotFoundMessage(int id)
        {
            PrintLine($"{id} is not found! try input again",ConsoleColor.Red);
        }
    }
}
