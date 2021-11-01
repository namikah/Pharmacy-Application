using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Aptek.Models;
using Aptek.Util;

namespace Aptek
{
    class Program
    {
        public static string CurrentUserName;
        public static string CurrentPassword;

        static void Main()
        {
            List<User> userList = new List<User>();

            while (CurrentUserName == null && CurrentPassword == null)
            {
                MenuLoginPanel(userList);
            }

            List<Pharmacy> pharmacyList = new();

            CustomAdd(pharmacyList);

            MainMenu(pharmacyList,userList);
        }

        public static bool CheckStatus(List<User> userList)
        {
            var IsAdmin = userList.FindAll(x => x.UserName == CurrentUserName && x.Password==CurrentPassword && x.Status == "Admin");
            if(IsAdmin.Count != 0)
            {
                return true;
            }
            return false;
        }

        public static void MenuAdminPanel(List<User> userList)
        {
        MenuAdminPanel:
            ShowAllUSer(userList);
            Helper.PrintSlowMotion(10, $"{Environment.NewLine}" +
                $"1. Add User{Environment.NewLine}" +
                $"2. Change user status{Environment.NewLine}" +
                $"3. Remove user{Environment.NewLine}" +
                $"4. Show all user{Environment.NewLine}" +
                $"5. Back to Main menu", ConsoleColor.Yellow);

            Helper.Print("Please select:", ConsoleColor.White);
            if (!int.TryParse(Console.ReadLine(), out int result))
            {
                Helper.IncorrectMessage();
                Console.Clear();
                return;
            }

            if (result == 1)
            {
                Helper.PrintSlowMotion(10, "User name (Ex: User or 123):", ConsoleColor.Yellow);
                string userName = Console.ReadLine();

                var drug = userList.Find(x => x.UserName == userName);

                if (drug != null)
                {
                    Helper.PrintSlowMotion(10, "Username already exist.", ConsoleColor.Red);
                    goto MenuAdminPanel;
                }

            inputPassword:
                Helper.PrintSlowMotion(10, "Password (ex: User123):", ConsoleColor.Yellow);
                string password = Console.ReadLine();

                Helper.PrintSlowMotion(10, "re-Password:", ConsoleColor.Yellow);
                string rePassword = Console.ReadLine();

                if (password != rePassword)
                {
                    Helper.PrintSlowMotion(10, "Password is not same", ConsoleColor.Red);
                    goto inputPassword;
                }

                User user = new User(userName, password, "User");
                userList.Add(user);

                Helper.PrintSlowMotion(10, $"User [{userName}] successfully added", ConsoleColor.Green);
                goto MenuAdminPanel;
            }

            else if (result == 2)
            {
                Console.Write("Select user:");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Helper.IncorrectMessage();
                    Console.Clear();
                    goto MenuAdminPanel;
                }

                var User = userList.Find(x => x.Id == id);
                if (User.Status == "User")
                {
                    User.Status = "Admin";
                    Helper.PrintSlowMotion(10, $"status successfully changed to [Admin] for {User.UserName} ", ConsoleColor.Green);
                }
                else
                {
                    User.Status = "User";
                    Helper.PrintSlowMotion(10, $"status successfully changed to [User] for {User.UserName} ", ConsoleColor.Green);
                }
                Console.Clear();
                goto MenuAdminPanel;
            }
            else if (result == 3)
            {
                Console.WriteLine("Select user:");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Helper.IncorrectMessage();
                    Console.Clear();
                    goto MenuAdminPanel;
                }

                if (!RemoveUser(userList, id))
                {
                    Helper.PrintSlowMotion(10, "User not removed", ConsoleColor.Red);
                    goto MenuAdminPanel;
                }
                Helper.PrintSlowMotion(10, "User successfully removed", ConsoleColor.Green);
                if (userList.Count == 0)
                {
                    CurrentUserName = null;
                    CurrentPassword = null; 
                    Thread.Sleep(1000);
                    Console.Clear();
                    Main();
                }
                goto MenuAdminPanel;
            }
            else if(result == 4)
            {
                goto MenuAdminPanel;
            }

            else if (result == 5)
            {
                Console.Clear();
                return;
            }
        }

        public static bool RemoveUser(List<User> userList, int id)
        {
            var user = userList.Find(x => x.Id == id);
            if (user == null)
                return false;

            userList.Remove(user);
            return true;
        }

        public static void ShowAllUSer(List<User> userList)
        {
            if (userList.Count == 0)
            {
                return;
            }

            foreach (var u in userList)
            {
                Helper.PrintLine("".PadLeft(Console.WindowWidth, '-'), ConsoleColor.DarkMagenta);
                Helper.PrintLine(u, ConsoleColor.Yellow);
            }
        }

        public static void MenuLoginPanel(List<User> userList)
        {
        LoginMenu:
            Helper.PrintSlowMotion(10, $"1. Login{Environment.NewLine}2. Registration",ConsoleColor.Yellow);
            Helper.Print("Please select:",ConsoleColor.White);
            if (!int.TryParse(Console.ReadLine(), out int result))
            {
                Helper.IncorrectMessage();
                goto LoginMenu;
            }
            if(result == 1)
            {
                MenuLogin(userList);
            }
            else if(result == 2)
            {
                MenuRegistration(userList);
            }

            if (CurrentUserName == null || CurrentPassword==null)
                goto LoginMenu;
        }

        public static void MenuLogin(List<User> userList)
        {
            if (userList.Count == 0)
            {
               Helper.PrintSlowMotion(10, "No registration. Please register:", ConsoleColor.Red);
                MenuRegistration(userList);
            }

            InputUserName:
            Helper.PrintSlowMotion(10, "User name:", ConsoleColor.Yellow);
            string userName = Console.ReadLine();

            Helper.PrintSlowMotion(10,"Password:", ConsoleColor.Yellow);
            string password = Console.ReadLine();

            var isUser = userList.FindAll(x=>x.UserName==userName && x.Password ==password);

            if(isUser.Count == 0)
            {
                Helper.PrintSlowMotion(10, "Incorrect username or password", ConsoleColor.Red);
                goto InputUserName;
            }
            foreach (var item in userList)
            {
                if (item.UserName == userName && item.Password == password)
                {
                   Helper.PrintSlowMotion(10,"Welcome, " + userName, ConsoleColor.Green);
                    CurrentUserName = userName;
                    CurrentPassword = password;
                }
            }
            Console.Clear();
        }

        public static void MenuRegistration(List<User> userList)
        {
        InputUserName:
            Helper.PrintSlowMotion(10, "User name (Ex: User or 123):", ConsoleColor.Yellow);
            string userName = Console.ReadLine();

            var drug = userList.Find(x => x.UserName == userName);

            if (drug != null)
            {
                Helper.PrintSlowMotion(10, "Username already exist.",ConsoleColor.Red);
                goto InputUserName;
            }

        inputPassword:
            Helper.PrintSlowMotion(10,"Password (ex: User123):", ConsoleColor.Yellow);
            string password = Console.ReadLine();

            Helper.PrintSlowMotion(10, "re-Password:", ConsoleColor.Yellow);
            string rePassword = Console.ReadLine();

            if (password != rePassword)
            {
                Helper.PrintSlowMotion(10, "Password is not same", ConsoleColor.Red);
                goto inputPassword;
            }

            string status;
            if (userList.Count == 0)
            {
                status = "Admin";
            }
            else
            {
                status = "User";
            }
            User user = new User(userName, password, status);

            if (user._password == null || user.UserName == null)
            {
                Helper.PrintSlowMotion(10, "Incorrect username or password", ConsoleColor.Red);
                goto InputUserName;
            }

            userList.Add(user);
            CurrentUserName = userName;
            CurrentPassword = password;
            Helper.PrintSlowMotion(10, "registration succesfully", ConsoleColor.Green);
            Helper.PrintSlowMotion(10, $"Please login {CurrentUserName}", ConsoleColor.Yellow);
        }

        public static void MainMenu(List<Pharmacy> pharmacyList, List<User> userList)
        {
            while (true)
            {
                Thread.Sleep(500);
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
                            CreatePharmacy(pharmacyList);
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
                            RemoveDrug(pharmacyList);
                            break;

                        case Operations.SaleDrug:
                            Console.Clear();
                            SaleDrug(pharmacyList);
                            break;

                        case Operations.SearchDrug:
                            Console.Clear();
                            SearchDrug(pharmacyList);
                            break;

                        case Operations.UpdateDrug:
                            Console.Clear();
                            UpdateDrug(pharmacyList);
                            break;

                        case Operations.InfoDrug:
                            Console.Clear();
                            InfoDrug(pharmacyList);
                            break;

                        case Operations.AdminPanel:
                            Console.Clear();
                            if (CheckStatus(userList))
                            {
                                MenuAdminPanel(userList);
                            }
                            else
                            {
                                Console.WriteLine("Access denied");
                            }
                            break;

                        case Operations.LogOut:
                            Console.Clear();
                            LogOut(userList);
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

        public static void LogOut(List<User> userList)
        {
            CurrentUserName = null;
            CurrentPassword = null;
            MenuLoginPanel(userList);
        }

        public static void InfoDrug(List<Pharmacy> pharmacyList)
        {
            Helper.PrintSlowMotion(10, "Please enter drug name:",ConsoleColor.Yellow);
            string drugName = Console.ReadLine();

            Drug drug = null;
            foreach (var p in pharmacyList)
            {
                foreach (var d in p.DrugsList())
                {
                    if (d.Name.ToLower().Contains(drugName.ToLower()))
                    {
                        drug = d;
                        break;
                    }
                }
            }

            string infoPrice;
            if (drug.Price > 50)
            {
                infoPrice = "Cheap";
            }
            infoPrice = "expensive";

           Helper.PrintSlowMotion(10,$"{drug.Name} - is used for {drug.Type}. We have {drug.Quantity} pieces. {drug.Name} is also {infoPrice} drug. But we sell that only {drug.Price}AZN. Date expired at {drug.ExpirationTime.ToShortDateString()}",ConsoleColor.Green);
        }

        public static void SearchDrug(List<Pharmacy> pharmacyList)
        {
            if (pharmacyList.Count == 0)
            {
                Helper.PharmacyEmphtyMessage();
                return;
            }

            Pharmacy selectedPharmacy = MenuSelectPharmacy(pharmacyList);

            string drugName = MenuInputDrugName();

            var FoundList = selectedPharmacy.SearchDrug(drugName);
            if (FoundList.Count == 0)
            {
                Helper.PrintLine("Search result: Nothing found", ConsoleColor.Red);
                return;
            }

            Helper.PrintLine("Search result:", ConsoleColor.Green);
            Helper.PrintLine(selectedPharmacy, ConsoleColor.Black, ConsoleColor.Magenta);
            foreach (var drugs in FoundList)
            {
                Helper.PrintLine(drugs, ConsoleColor.Black, ConsoleColor.Yellow);
            }
        }

        public static void UpdateDrug(List<Pharmacy> pharmacyList)
        {
            if (pharmacyList.Count == 0)
            {
                Helper.PharmacyEmphtyMessage();
                return;
            }

            InputSelectPharmacy:
            Pharmacy selectedPharmacy = MenuSelectPharmacy(pharmacyList);

            List<Drug> drugList = selectedPharmacy.DrugsList();

            ShowDrugsByPharmacy(pharmacyList,selectedPharmacy);
            var selectedDrug = MenuInputSelectDrugById(drugList);
            var oldDrugName = selectedDrug.Name;
            var oldDrugPrice = selectedDrug.Price;
            
            if (!IsEmphtyDrugs(drugList))
                    goto InputSelectPharmacy;

        SelectUpdateBy:
            var selectedUpdateBy = MenuInputUpdateBy();

            if (selectedUpdateBy != 1 && selectedUpdateBy != 2)
            {
                Helper.IncorrectMessage();
                goto SelectUpdateBy;
            }
            else if (selectedUpdateBy == 1)
            {
                var drugName = MenuInputDrugName();
                selectedPharmacy.Update(selectedDrug, drugName);
                Helper.PrintSlowMotion(10, $"Old drug name [{oldDrugName}] successfully changed to new [{drugName}]",ConsoleColor.Green);
                return;
            }
            else if (selectedUpdateBy == 2)
            {
                var drugPrice = MenuInputDrugPrice();
                selectedPharmacy.Update(selectedDrug, null, drugPrice);
                Helper.PrintSlowMotion(10, $"Old drug price [{oldDrugPrice}] successfully changed to new [{drugPrice}]", ConsoleColor.Green);
            }
        }

        public static int MenuInputUpdateBy()
        {
            InputShooseUpdate:
            Helper.PrintLine($"Please choose to change:{Environment.NewLine}" +
                $"1. Drug name{Environment.NewLine}" +
                $"2. Drug price", ConsoleColor.Yellow);

            if (!int.TryParse(Console.ReadLine(), out int result))
            {
                Helper.IncorrectMessage();
                goto InputShooseUpdate;
            }

            return result;
        }

        public static void MenuCreateDrug(List<Pharmacy> pharmacyList)
        {
            if (!IsEmphtyPharmacy(pharmacyList))
            {
                CreatePharmacy(pharmacyList);
            }

            Pharmacy selectedPharmacy = MenuSelectPharmacy(pharmacyList);

            var drugName = MenuInputDrugName();

            var selectedDrug = selectedPharmacy.DrugsList().Find(x => x.Name == drugName);

            if (selectedPharmacy.IsExistDrug(drugName))
            {
                Helper.PrintLine($"{drugName} is already exist. Do you want to add quantity? [y/n]",ConsoleColor.Red);
                AddQuantity(selectedPharmacy, selectedDrug);
                return;
            }

            var drugType = MenuInputDrugType();

            var price = MenuInputDrugPrice();

            var drugQuantity = MenuInputQuantityAdd();

            var date = MenuInputDateTime();

            selectedPharmacy.AddDrug(new Drug(drugName, drugType, drugQuantity, price, date));
            Helper.PrintSlowMotion(10, $"[{drugName}] is successfully added to [{selectedPharmacy.Name}]", ConsoleColor.Green);
        }

        public static string MenuInputDrugName()
        {
            Helper.Print("Enter drug name:", ConsoleColor.White);
            return Console.ReadLine();
        }

        public static string MenuInputPharmacyName(List<Pharmacy> pharmacyList)
        {
        InputPharmacy:
            Helper.Print("Enter Pharmacy Name:", ConsoleColor.White);
            string name = Console.ReadLine();

            if (pharmacyList.Exists(x => x.Name.ToLower() == name.ToLower()))
            {
                Helper.PrintLine($"Pharmacy name [{name}] already exist", ConsoleColor.Red);
                goto InputPharmacy;
            }
            return name;
        }

        public static int MenuInputQuantityAdd()
        {
        inputDrugQuantity:
            Helper.Print("Enter drug quantity:", ConsoleColor.White);
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Helper.IncorrectMessage();
                goto inputDrugQuantity;
            }
            if (quantity <= 0)
            {
                Helper.PrintLine("Please enter valid number, more than zero", ConsoleColor.Red);
                goto inputDrugQuantity;
            }
            return quantity;
        }

        public static DateTime MenuInputDateTime()
        {
        inputDrugExDate:
            Helper.PrintLine("Enter drug's expiration time (format:12/13/2001):", ConsoleColor.White);
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date) && date < DateTime.Today)
            {
                Helper.IncorrectMessage();
                goto inputDrugExDate;
            }
            return date;
        }

        public static DrugType MenuInputDrugType()
        {
            Helper.Print("Enter drug type:", ConsoleColor.White);
            return new DrugType(Console.ReadLine());
        }

        public static double MenuInputDrugPrice()
        {
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
            return price;
        }

        public static void CreatePharmacy(List<Pharmacy> pharmacyList)
        {
            var name = MenuInputPharmacyName(pharmacyList);

            pharmacyList.Add(new Pharmacy(name));

            Helper.PrintSlowMotion(10, $"[{name}] Created successfully", ConsoleColor.Green);
        }

        public static void AddQuantity(Pharmacy selectedPharmacy, Drug selectedDrug)
        {
            if (Console.ReadLine() == "y")
            {
            InputDrugQuantity:
                Helper.Print("Enter drug count:", ConsoleColor.White);
                if (!int.TryParse(Console.ReadLine(), out int Quantity))
                {
                    Helper.IncorrectMessage();
                    goto InputDrugQuantity;
                }
                if (Quantity <= 0)
                {
                    Helper.PrintLine("Please enter valid number, more than zero", ConsoleColor.Red);
                    goto InputDrugQuantity;
                }
                selectedDrug.IncrementQuantity(Quantity);
                Helper.PrintSlowMotion(10, $"{Quantity} {selectedDrug.Name} successfully added to {selectedPharmacy.Name}", ConsoleColor.Green);
            }else
            {
                return;
            }
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
        
        public static void RemoveDrug(List<Pharmacy> pharmacyList)
        {
            if (!IsEmphtyPharmacy(pharmacyList))
            {
                return;
            }

            InputSelectPharmacy:
            Pharmacy selectedPharmacy = MenuSelectPharmacy(pharmacyList);

            var drugList = selectedPharmacy.DrugsList();
            if (!IsEmphtyDrugs(drugList))
            {
                goto InputSelectPharmacy;
            }

        inputDrugId:
            ShowDrugsByPharmacy(pharmacyList,selectedPharmacy);
            Helper.Print("Select drug to remove:", ConsoleColor.White);
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Helper.IncorrectMessage();
                goto inputDrugId;
            }

            var RemovedDrugName = selectedPharmacy.DrugsList().Find(x => x.Id == id).Name;
            if (!selectedPharmacy.RemoveDrug(id))
            {
                Helper.IdNotFoundMessage(id);
                goto inputDrugId;
            }

            Helper.PrintSlowMotion(10, $"[{RemovedDrugName}] is successfully removed from [{selectedPharmacy.Name}]", ConsoleColor.Green);
        }

        public static void SaleDrug(List<Pharmacy> pharmacyList)
        {
            if (!IsEmphtyPharmacy(pharmacyList))
            {
                return;
            }

        InputSelectPharmacy:
            Pharmacy selectedPharmacy = MenuSelectPharmacy(pharmacyList);

            var drugList = selectedPharmacy.DrugsList();
            if (!IsEmphtyDrugs(drugList))
            {
                goto InputSelectPharmacy;
            }

            ShowDrugsByPharmacy(pharmacyList, selectedPharmacy);
            var selectedDrug = MenuInputSelectDrugById(drugList);
            if (selectedDrug.Quantity == 0)
            {
                Helper.PrintLine($"Sorry, but we have not [{selectedDrug.Name}]", ConsoleColor.Red);
                return;
            }
            if (selectedDrug.ExpirationTime < DateTime.Today)
            {
                Helper.PrintLine($"Sorry, but [{selectedDrug.Name}] is expired", ConsoleColor.Red);
                return;
            }

            Helper.PrintLine($"{selectedDrug.Name} is selected", ConsoleColor.Yellow);
            var quantity = MenuInputQuantitySale(selectedDrug);

            PayMoney(selectedDrug,quantity);
        }

        public static int MenuInputQuantitySale(Drug selectedDrug)
        {
        InputDrugQuantity:
            Helper.Print("How much, you want to buy:", ConsoleColor.White);
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Helper.IncorrectMessage();
                goto InputDrugQuantity;
                //selectedDrug = MenuInputSelectDrugById(pharmacyList, drugList);
            }

            if (!CheckDrugQuantity(selectedDrug, quantity))
            {
                Helper.PrintLine($"Sorry, but we have only {selectedDrug.Quantity} pieces.", ConsoleColor.Red);
                goto InputDrugQuantity;
            }
            return quantity;
        }

        public static Drug MenuInputSelectDrugById(List<Drug> drugList)
        {
        inputDrugId:
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
            var selectedDrug = drugList.Find(x => x.Id == id);
            return selectedDrug;
        }

        public static void PayMoney(Drug selectedDrug, int quantity)
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
                Helper.PrintLine($"Sorry, you must pay {selectedDrug.Price * quantity}AZN ({selectedDrug.Price} * {quantity} = {selectedDrug.Price * quantity})", ConsoleColor.Red);
                goto InputMoney;
            }
            else if (money > selectedDrug.Price * quantity)
            {
                if (!selectedDrug.DecrementQuantity(quantity))
                {
                    goto InputMoney;
                }
                Helper.PrintSlowMotion(10, $"You have paid more than [{selectedDrug.Price * quantity}AZN]. please take [{money - selectedDrug.Price * quantity}AZN] Thank you", ConsoleColor.Green);

            }
            else if (money == selectedDrug.Price * quantity)
            {
                if (!selectedDrug.DecrementQuantity(quantity))
                {
                    goto InputMoney;
                }
                Helper.PrintSlowMotion(10, $"Thank you, please take drugs", ConsoleColor.Green);
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

        public static void ShowDrugsByPharmacy(List<Pharmacy> pharmacyList,Pharmacy selectedPharmacy)
        {
            if (pharmacyList.Count == 0)
            {
                Helper.PharmacyEmphtyMessage();
                return;
            }

            foreach (var p in pharmacyList)
            {
                if (p != selectedPharmacy)
                {
                    continue;
                }

                Helper.PrintLine("".PadLeft(Console.WindowWidth, '-'), ConsoleColor.DarkMagenta);
                Helper.PrintLine(p, ConsoleColor.Yellow);

                foreach (var d in p.DrugsList())
                {
                        Helper.PrintLine(d, ConsoleColor.DarkYellow);
                }
                break;
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
            Helper.PrintSlowMotion(5,"".PadLeft(Console.WindowWidth, '=') + Environment.NewLine, ConsoleColor.DarkMagenta);
            Helper.PrintSlowMotion(1,
                $"[1] Create Pharmacy{Environment.NewLine}" +
                $"[2] Create Drug{Environment.NewLine}" +
                $"[3] Show all drugs{Environment.NewLine}" +
                $"[4] Remove drug{Environment.NewLine}" +
                $"[5] Sale drug{Environment.NewLine}" +
                $"[6] Search drug{Environment.NewLine}" +
                $"[7] Update Drug{Environment.NewLine}" +
                $"[8] Drug info{Environment.NewLine}" +
                $"[9] Admin panel{Environment.NewLine}" +
                $"[10] Log out{Environment.NewLine}" +
                $"[11] Exit", ConsoleColor.Yellow);
            Helper.PrintLine("".PadLeft(Console.WindowWidth, '=') + Environment.NewLine, ConsoleColor.DarkMagenta);
            Helper.PrintLine("Select Operation:" + Environment.NewLine, ConsoleColor.White);
        }

        public static void CustomAdd(List<Pharmacy> pharmacyList)
        {
            Pharmacy p1 = new("OZON aptek");
            Pharmacy p2 = new("ZEFERAN aptek");
            Pharmacy p3 = new("AVIS aptek");
            Pharmacy p4 = new("ZEYTUN aptek");

            pharmacyList.Add(p1);
            pharmacyList.Add(p2);
            pharmacyList.Add(p3);
            pharmacyList.Add(p4);

            p1.AddDrug(new Drug("Cardio-Maqnil", new DrugType("cardiovascular"), 5, 20, DateTime.Today.AddDays(-5)));
            p1.AddDrug(new Drug("Evinol", new DrugType("Vitamin"), 10, 12, DateTime.Today));
            p2.AddDrug(new Drug("Ekvator", new DrugType("cardiovascular"), 7, 35, DateTime.Today));
            p2.AddDrug(new Drug("MultiVita", new DrugType("Vitamin"), 12, 25, DateTime.Today.AddDays(-5)));
            p3.AddDrug(new Drug("Norkalut", new DrugType("Ginecology"), 8, 50, DateTime.Today));
            p3.AddDrug(new Drug("Novanfron", new DrugType("kidney disease"), 6, 16, DateTime.Today));
            p4.AddDrug(new Drug("cistilen", new DrugType("kidney disease"), 45, 12.50, DateTime.Today.AddDays(-5)));
            p4.AddDrug(new Drug("Aspirin", new DrugType("Vitamin"), 5, 8.50, DateTime.Today));
        }

        public static bool IsEmphtyPharmacy(List<Pharmacy> pharmacyList)
        {
            if (pharmacyList.Count != 0)
            {
                return true;
            }
            Helper.PharmacyEmphtyMessage();
            return false;
        }

        public static bool IsEmphtyDrugs(List<Drug> drugList)
        {
            if (drugList.Count != 0)
            {
                return true;
            }
            Helper.PrintLine($"No drug found", ConsoleColor.Red);
            return false;
        }

        public static bool CheckDrugExTime(Drug drug)
        {
            if(drug.ExpirationTime >= DateTime.Today)
            {
                return true;
            }
            return false;
        }
    }
}
