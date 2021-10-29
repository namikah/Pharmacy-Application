using Aptek.Models;
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

        public static void PharmacyEmphtyMessage()
        {
            Helper.PrintLine("Pharmacy is emphty", ConsoleColor.Red);
        }

        public static void IdNotFoundMessage(int id)
        {
            PrintLine($"{id} is not found! try input again",ConsoleColor.Red);
        }

        //public static void SorryNotQuantityMessage(Drug drug, int quantity)
        //{
        //    PrintLine($"Sorry, but we have only {drug.Quantity} pieces.", ConsoleColor.Red);
        //}

        public static bool IsEmphtyPharmacy(List<Pharmacy> pharmacyList)
        {
            if (!(pharmacyList.Count == 0))
            {
                return true;
            }
            return false;
        }
    }
}
