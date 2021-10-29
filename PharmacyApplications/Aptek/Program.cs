using System;
using System.Collections.Generic;
using System.Threading;
using Aptek.Models;
using Aptek.Util;

namespace Aptek
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pharmacy> pharmacyList = new List<Pharmacy>();

            MainMenuDesign();

            MainMenu(pharmacyList);
        }

        public static void MainMenu(List<Pharmacy> pharmacyList)
        {
            while (true)
            {
                Console.Title = "PHARMACY APPLICATION";
                Helper.PrintLine("".PadLeft(Console.WindowWidth, '='), ConsoleColor.DarkMagenta);
                Helper.PrintLine(
                    $"[1] Create Pharmacy{Environment.NewLine}" +
                    $"[2] Create Drug{Environment.NewLine}" +
                    $"[3] Show all drugs{Environment.NewLine}" +
                    $"[4] Remove drug{Environment.NewLine}" +
                    $"[5] Remove pharmacy{Environment.NewLine}" +
                    $"[6] Sale drug{Environment.NewLine}" +
                    $"[7] Exit", ConsoleColor.Yellow);
                Helper.PrintLine("".PadLeft(Console.WindowWidth, '='), ConsoleColor.DarkMagenta);
                Helper.Print("Select Operation:",ConsoleColor.White);
                bool isInt = int.TryParse(Console.ReadLine(), out int menu);
                int operationCount = Enum.GetValues(typeof(Operations)).Length;
                if (isInt && (menu >= 1 && menu <= operationCount))
                {
                    if (menu == operationCount)
                        break;

                    switch ((Operations)menu)
                    {
                        case Operations.CreatePharmacy:
                            Console.Clear();
                            MenuCreatePharmacy(pharmacyList);
                            break;

                        case Operations.CreateDrug:
                            Console.Clear();
                            MenuCreateDrug(pharmacyList);
                            break;

                        case Operations.ShowAllDrugs:
                            Console.Clear();
                            ShowAllDrugs(pharmacyList);
                            break;

                        case Operations.RemoveDrug:
                            Console.Clear();
                            break;

                        case Operations.RemovePharmacy:
                            Console.Clear();
                            break;

                        case Operations.SaleDrug:
                            Console.Clear();
                            break;

                        case Operations.Exit:
                            Console.Clear();
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    Helper.IncorrectMessage();
                }
            }
        }

        public static void MenuCreatePharmacy(List<Pharmacy> pharmacyList)
        {
        InputPharmacy:
            Helper.Print("Enter Pharmacy Name:", ConsoleColor.White);
            string name = Console.ReadLine();

            if(pharmacyList.Exists(x => x.Name.ToLower() == name.ToLower()))
            {
                Helper.PrintLine("Pharmacy name already exist", ConsoleColor.Red);
                goto InputPharmacy;
            }

            pharmacyList.Add(new Pharmacy(name));

            Helper.PrintLine($"{name} Created successfully", ConsoleColor.Green);
        }

        public static void MenuCreateDrug(List<Pharmacy> pharmacyList)
        {
            if (pharmacyList.Count == 0)
            {
                Helper.PrintLine("No available pharmacy found", ConsoleColor.Red);
                MenuCreatePharmacy(pharmacyList);
            }

        selectPharmacy:
            ShowAllPharmacies(pharmacyList);
            Helper.Print("Select pharmacy:", ConsoleColor.Yellow);
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Helper.IncorrectMessage();
                goto selectPharmacy;
            }

            var selectedPharmacy = pharmacyList.Find(x => x.Id == id);
            if (selectedPharmacy == null)
            {
                Console.WriteLine($"{id} is not found! try input again");
                goto selectPharmacy;
            }

            Helper.Print("Enter drug name:", ConsoleColor.White);
            string name = Console.ReadLine();

            Helper.Print("Enter drug type:", ConsoleColor.White);
            string typeName = Console.ReadLine();

        inputDrugPrice:
            Helper.Print("Enter drug price:", ConsoleColor.White);
            if(!double.TryParse(Console.ReadLine(), out double price) && price < 0)
            {
                Helper.IncorrectMessage();
                goto inputDrugPrice;
            }

        inputDrugCount:
            Helper.Print("Enter drug count:", ConsoleColor.White);
            if (!int.TryParse(Console.ReadLine(), out int count))
            {
                Helper.IncorrectMessage();
                goto inputDrugCount;
            }
            if (count == 0)
            {
                Helper.PrintLine("Please enter valid number, more than zero", ConsoleColor.Red);
                goto inputDrugCount;
            }

        inputDrugExDate:
            Helper.PrintLine("Enter drug's expiration time (format:12/13/2001):", ConsoleColor.White);
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date) && date < DateTime.Today)
            {
                Helper.IncorrectMessage();
                goto inputDrugExDate;
            }

            selectedPharmacy.AddDrug(new Drug(name, new DrugType(typeName), count, price, date));
            Helper.PrintLine($"<{name}> is successfully added to <{selectedPharmacy.Name}>", ConsoleColor.Green);
        }

        public static void ShowAllDrugs(List<Pharmacy> pharmacyList)
        {
            if(pharmacyList.Count == 0)
            {
               Helper.PrintLine("Pharmacy is emphty",ConsoleColor.Red);
                return;
            }

            foreach (var p in pharmacyList)
            {
                if (p.ShowDrugs().Count == 0)
                    continue;

                Helper.PrintLine("".PadLeft(Console.WindowWidth, '-'), ConsoleColor.DarkMagenta);
                Helper.PrintLine(p, ConsoleColor.Yellow);
                foreach (var d in p.ShowDrugs())
                {
                    Helper.PrintLine(d, ConsoleColor.DarkYellow);
                }
            }
        }
        public static void ShowAllPharmacies(List<Pharmacy> pharmacyList)
        {
            foreach (var item in pharmacyList)
            {
                Helper.PrintLine(item, ConsoleColor.Yellow);
            }
        }

        public static void MainMenuDesign()
        {
            Helper.PrintLine("".PadLeft(Console.WindowWidth, '='), ConsoleColor.DarkMagenta);
            Helper.PrintLine(":: P H A R M A C Y   A P P L I C A T I O N ::".PadLeft(Console.WindowWidth/2 + 20), ConsoleColor.Yellow);
            Helper.PrintLine("".PadLeft(Console.WindowWidth, '-'), ConsoleColor.DarkYellow);
            Helper.PrintLine("".PadLeft(Console.WindowWidth, '='), ConsoleColor.DarkMagenta);
            Console.WriteLine(":: MAIN MENU ::".PadLeft(Console.WindowWidth / 2 + 5), ConsoleColor.Yellow);
            Helper.PrintLine("".PadLeft(Console.WindowWidth, '-'), ConsoleColor.DarkYellow);
            //Console.WriteLine();
            //Console.WriteLine(" >> AddMedicine    [1]");
            //Console.WriteLine(" >> EditMedicine   [2]");
            //Console.WriteLine(" >> UpdateAmount   [3]");
            //Console.WriteLine(" >> DeleteMedicine [4]");
            //Console.WriteLine(" >> ShowMedicines  [5]");
            //Console.WriteLine(" >> SellMedicine   [6]");
            //Console.WriteLine(" >> Help           [7]");
            //Console.WriteLine(" >> Exit           [8]");
            Helper.PrintLine("".PadLeft(Console.WindowWidth, '='), ConsoleColor.DarkMagenta);
        }
    }
}
