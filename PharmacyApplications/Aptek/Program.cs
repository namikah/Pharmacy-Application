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

            MainMenu(pharmacyList);
        }

        public static void MainMenu(List<Pharmacy> pharmacyList)
        {
            while (true)
            {
                MainMenuDesign();
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
                            MenuRemoveDrug(pharmacyList);
                            break;

                        case Operations.SaleDrug:
                            Console.Clear();
                            MenuSaleDrug(pharmacyList);
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
                Helper.PrintLine($"Pharmacy name [{name}] already exist", ConsoleColor.Red);
                goto InputPharmacy;
            }

            pharmacyList.Add(new Pharmacy(name));

            Helper.PrintLine($"[{name}] Created successfully", ConsoleColor.Green);
        }

        public static void MenuCreateDrug(List<Pharmacy> pharmacyList)
        {
            if (!Helper.IsEmphtyPharmacy(pharmacyList))
            {
                Helper.PharmacyEmphtyMessage();
                MenuCreatePharmacy(pharmacyList);
            }

            Pharmacy selectedPharmacy = MenuSelectPharmacy(pharmacyList);

            Helper.Print("Enter drug name:", ConsoleColor.White);
            string name = Console.ReadLine();

            Helper.Print("Enter drug type:", ConsoleColor.White);
            string typeName = Console.ReadLine();

        inputDrugPrice:
            Helper.Print("Enter drug price:", ConsoleColor.White);
            double price;
            try 
            { 
                price = Convert.ToDouble(Console.ReadLine()); 
            } 
            catch
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
            Helper.PrintLine($"[{name}] is successfully added to [{selectedPharmacy.Name}]", ConsoleColor.Green);
        }

        public static Pharmacy MenuSelectPharmacy(List<Pharmacy> pharmacyList)
        {
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
                Helper.IdNotFoundMessage(id);
                goto selectPharmacy;
            }

            return selectedPharmacy;
        }
        
        public static void MenuRemoveDrug(List<Pharmacy> pharmacyList)
        {
            if (!Helper.IsEmphtyPharmacy(pharmacyList))
            {
                Helper.PharmacyEmphtyMessage();
                return;
            }

            InputSelectPharmacy:
            Pharmacy selectedPharmacy = MenuSelectPharmacy(pharmacyList);

            var drugList = selectedPharmacy.DrugsList();
            if (drugList.Count == 0)
            {
                Helper.PrintLine($"No drug in [{selectedPharmacy.Name}]", ConsoleColor.Red);
                goto InputSelectPharmacy;
            }

        inputDrugId:
            ShowAllDrugs(pharmacyList);
            Helper.Print("Select drug to remove:", ConsoleColor.White);
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Helper.IncorrectMessage();
                goto inputDrugId;
            }

            if (!selectedPharmacy.RemoveDrug(id))
            {
                Helper.IdNotFoundMessage(id);
                goto inputDrugId;
            }

            //var removedDrugName = selectedPharmacy.DrugsList().Find(x => x.Id == id).Name;
            //Helper.PrintLine($"[{removedDrugName}] is successfully removed from [{selectedPharmacy.Name}]", ConsoleColor.Green);
        }

        public static void MenuSaleDrug(List<Pharmacy> pharmacyList)
        {
            if (!Helper.IsEmphtyPharmacy(pharmacyList))
            {
                Helper.PharmacyEmphtyMessage();
                return;
            }

        InputSelectPharmacy:
            Pharmacy selectedPharmacy = MenuSelectPharmacy(pharmacyList);

            var drugList = selectedPharmacy.DrugsList();
            if (drugList.Count == 0)
            {
                Helper.PrintLine($"No drug in [{selectedPharmacy.Name}]", ConsoleColor.Red);
                goto InputSelectPharmacy;
            }

            MenuInputDrugId(pharmacyList,drugList, out Drug selectedDrug);

            MenuInputDrugQuantity(pharmacyList, drugList, selectedDrug, out int quantity);

            MenuPayMoney(selectedDrug,quantity);
        }

        public static void MenuInputDrugQuantity(List<Pharmacy> pharmacyList, List<Drug> drugList, Drug selectedDrug, out int quantity)
        {
        InputDrugQuantity:
            Helper.Print("How much, you want to buy:", ConsoleColor.White);
            if (!int.TryParse(Console.ReadLine(), out quantity))
            {
                Helper.IncorrectMessage();
                MenuInputDrugId(pharmacyList, drugList, out selectedDrug);
            }

            if (!CheckDrugQuantity(selectedDrug, quantity))
            {
                Helper.PrintLine($"Sorry, but we have only {selectedDrug.Quantity} pieces.", ConsoleColor.Red);
                goto InputDrugQuantity;
            }
        }

        public static void MenuInputDrugId(List<Pharmacy> pharmacyList, List<Drug> drugList, out Drug selectedDrug)
        {
        inputDrugId:
            ShowAllDrugs(pharmacyList);
            Helper.Print("Select drug:", ConsoleColor.White);
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Helper.IncorrectMessage();
                goto inputDrugId;
            }

            if (!drugList.Exists(x => x.Id == id))
            {
                Helper.IdNotFoundMessage(id);
                goto inputDrugId;
            }
            selectedDrug = drugList.Find(x => x.Id == id);
            if (selectedDrug.Quantity == 0)
            {
                Helper.PrintLine($"Sorry, but we have not [{selectedDrug.Name}]", ConsoleColor.Red);
                return;
            }
        }

        public static void MenuPayMoney(Drug selectedDrug, int quantity)
        {
        InputMoney:
            Helper.PrintLine($"Please pay {selectedDrug.Price * quantity}AZN ({selectedDrug.Price} * {quantity} = {selectedDrug.Price * quantity})", ConsoleColor.White);
            if (!double.TryParse(Console.ReadLine(), out double money))
            {
                Helper.IncorrectMessage();
                goto InputMoney;
            }
            if (money < selectedDrug.Price * quantity)
            {
                Helper.PrintLine($"Sorry, you must to pay {selectedDrug.Price * quantity}AZN ({selectedDrug.Price} * {quantity} = {selectedDrug.Price * quantity})", ConsoleColor.Red);
                goto InputMoney;
            }
            else if (money > selectedDrug.Price * quantity)
            {
                if (!selectedDrug.DecrementQuantity(quantity))
                {
                    goto InputMoney;
                }
                Helper.PrintLine($"You have paid more than [{selectedDrug.Price * quantity}]. please take [{money - selectedDrug.Price * quantity}AZN] Thank you", ConsoleColor.Red);
            }
            else if (money == selectedDrug.Price * quantity)
            {
                if (!selectedDrug.DecrementQuantity(quantity))
                {
                    goto InputMoney;
                }
                Helper.PrintLine($"Thank you, please take drugs", ConsoleColor.Red);
            }
        }

        public static bool CheckDrugQuantity(Drug selectedDrug, int quantity)
        {
            if(selectedDrug.Quantity >= quantity)
            {
                return true;
            }
            return false;
        }

        public static bool CheckDrugPrice(Drug selectedDrug, double money)
        {
            if (selectedDrug.Price == money)
            {
                return true;
            }
            else return false;
        }

        public static void ShowAllDrugs(List<Pharmacy> pharmacyList)
        {
            if(pharmacyList.Count == 0)
            {
                Helper.PharmacyEmphtyMessage();
                return;
            }

            foreach (var p in pharmacyList)
            {
                if (p.DrugsList().Count == 0)
                {
                    continue;
                }

                Helper.PrintLine("".PadLeft(Console.WindowWidth, '-'), ConsoleColor.DarkMagenta);
                Helper.PrintLine(p, ConsoleColor.Yellow);

                foreach (var d in p.DrugsList())
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
            Console.Title = "PHARMACY APPLICATION";
            Helper.PrintLine("".PadLeft(Console.WindowWidth, '='), ConsoleColor.DarkMagenta);
            Helper.PrintLine(
                $"[1] Create Pharmacy{Environment.NewLine}" +
                $"[2] Create Drug{Environment.NewLine}" +
                $"[3] Show all drugs{Environment.NewLine}" +
                $"[4] Remove drug{Environment.NewLine}" +
                $"[5] Sale drug{Environment.NewLine}" +
                $"[6] Exit", ConsoleColor.Yellow);
            Helper.PrintLine("".PadLeft(Console.WindowWidth, '='), ConsoleColor.DarkMagenta);
            Helper.Print("Select Operation:", ConsoleColor.White);
        }
    }
}
