using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BankingLedger
{
    public class Session
    {
        private Client currentUser; //Keeps track of current user
        private string currentAccount; //Keeps track of current account
        private List<Client> clientList; //Holds all clients
        private string heading = "The Best Banking Ledger Ever!\n\n";
        private string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\")) + "database.json"; //Path to Json file "database"

        private void JsonSave()
        {
            //Saving updated information to Json file - with formatting - Serializing
            string jsonReturn = JsonConvert.SerializeObject(clientList.ToArray(), Formatting.Indented);
            System.IO.File.WriteAllText(path, jsonReturn);
        }

        public void JsonRead()
        {
            if (File.Exists(path)) // only read if the file exists
            {
                //Read all Json file data "database" and storing in the list of Customers - De-Serializing
                var jsonData = System.IO.File.ReadAllText(path);
                clientList = JsonConvert.DeserializeObject<List<Client>>(jsonData);
            }
            else
            {
                clientList = new List<Client>(); //instatiate new client list
            }
        }


        public void run()
        {
            currentAccount = "Checking Account";
            string[] menuValues = { "C", "L", "Q" };
            string menuChoice = "";

            while (!menuValues.Contains(menuChoice)) //LINQ query operator
            {
                //Main Menu
                Console.Clear();
                Console.WriteLine($"{heading}\nWould you like to:\n\n");
                Console.WriteLine("C -> Create new account?  ");
                Console.WriteLine("L -> Login to your account? ");
                Console.WriteLine("Q -> Quit Session? \n\n");
                Console.Write("Enter choice: ");
                menuChoice = Console.ReadLine();
            }

            switch (menuChoice)
            {
                //Creating new account
                case "C":
                    createAccount();
                    JsonSave(); //Saving data
                    userMenu();
                    break;
                //Logging into account
                case "L":
                    login();
                    break;
                //Quitting Session
                case "Q":
                    JsonSave();
                    Console.Clear();
                    Console.WriteLine($"{heading}\nThank you for using our amazing bank, please come back again!\n\n");
                    Console.WriteLine("Shut...");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("       ...ing...");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("                ...down!\n\n");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("Bye Bye =)");
                    System.Threading.Thread.Sleep(1000);
                    break;
            }

        }

        //Logging into Account
        private void login()
        {
            int i = 0;
            bool set = false;

            Console.Clear();
            Console.WriteLine($"{heading}\nLet's Log In!\n");

            Console.Write("Enter Username: ");
            string userName = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            if (!clientList.Equals(null))
            {
                foreach (var obj in clientList)
                {
                    if (obj.userName.Equals(userName) && obj.password.Equals(password))
                    {
                        set = true;
                        currentUser = clientList.ElementAt(i);
                    }
                    i += 1;
                }
            }
            if (!set)
            {
                Console.WriteLine("\nYou entered the wrong Username and Password combination, please try again!\n\n");
                Console.Write("Press <Enter> to go back to menu...");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                run();
            }
            else
            {
                userMenu();
            }

        }

        private void createAccount()
        {
            string namePattern = "^[A-Z][a-z]+$";
            string usernamePattern = "^[A-Za-z0-9_-]{3,15}$";
            string passwordPattern = "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$"; 
            string inputValue = "";
            string firstName = "";
            string lastName = "";
            string userName = "";
            string password = "";

            Match m = Regex.Match(inputValue, namePattern);


            Console.Clear();
            Console.WriteLine($"{heading}\nLet's Create a New Account!\n");
            Console.WriteLine("[This will also create a new Checking Account for you]\n\n");
            while (!m.Success)
            {
                Console.Write("Enter First Name: ");
                inputValue = Console.ReadLine();
                m = Regex.Match(inputValue, namePattern);
                if (!m.Success)
                {
                    Console.WriteLine("\nFirst name must have upper case first letter and lower case letters only for the rest");
                    Console.WriteLine("Please try again!\n");
                }
            }
            firstName = inputValue;
            inputValue = "";
            m = Regex.Match(inputValue, namePattern);
            while (!m.Success)
            {
                Console.Write("Enter Last Name: ");
                inputValue = Console.ReadLine();
                m = Regex.Match(inputValue, namePattern);
                if (!m.Success)
                {
                    Console.WriteLine("\nLast name must have upper case first letter and lower case letters only for the rest");
                    Console.WriteLine("Please try again!\n");
                }
            }
            lastName = inputValue;
            inputValue = "";
            m = Regex.Match(inputValue, namePattern);
            while (!m.Success)
            {
                Console.Write("Enter Username: ");
                inputValue = Console.ReadLine();
                m = Regex.Match(inputValue, usernamePattern);
                if (!m.Success)
                {
                    Console.WriteLine("\nUsername must have 3-15 characters allowing upper case letters, lower case letters, numbers, underscores, and hyphens");
                    Console.WriteLine("Please try again!\n");
                }
            }
            userName = inputValue;
            inputValue = "";
            m = Regex.Match(inputValue, usernamePattern);
            while (!m.Success)
            {
                Console.Write("Enter Password: ");
                inputValue = Console.ReadLine();
                m = Regex.Match(inputValue, passwordPattern);
                if (!m.Success)
                {
                    Console.WriteLine("\nPassword must have a minimum of 8 characters, needs at least one number and one letter");
                    Console.WriteLine("Please try again!\n");
                }
            }
            password = inputValue;

            //Creating current user
            currentUser = new Client(firstName, lastName, userName, password);
            currentUser.createNewAccount("Checking Account");

            if (!File.Exists(path))
            {
                //Adding user to a List of Customers [Will write the List to json file on close]
                clientList = new List<Client>();
            }
            clientList.Add(currentUser);

        }

        private void userMenu()
        {
            string choice = "";
            string[] choices = { "C", "A", "D", "W", "V", "B", "L" }; //add all choices to array

            while (!choices.Contains(choice))
            {
                Console.Clear();
                Console.WriteLine($"{heading}\nWelcome {currentUser.firstName} {currentUser.lastName}! \n\n\n");


                Console.WriteLine($"[You are currently in your {currentAccount}]\n\n");


                Console.WriteLine("Would you like to:\n\n");
                Console.WriteLine("C -> Change to a different account? ");
                Console.WriteLine("A -> Create another account? [Savings, Loan, Credit Line]");
                Console.WriteLine($"D -> Deposit into your {currentAccount}? ");
                Console.WriteLine($"W -> Withdraw from your {currentAccount}? ");
                Console.WriteLine($"V -> View all {currentAccount} transactions? ");
                Console.WriteLine($"B -> Check {currentAccount} balance? ");
                Console.WriteLine("L -> Log out of account? \n\n");


                Console.Write("Enter choice: ");
                choice = Console.ReadLine();
            }


            switch (choice)
            {
                //Change to different accounts Ex. Checking, Savings, Loan, etc.
                case "C":

                    string changeChoice = "";
                    string[] changeChoices = { "C", "S", "L", "T", "B" }; //add all choices to array

                    while (!changeChoices.Contains(changeChoice)) //LINQ query operator
                    {
                        Console.Clear();
                        Console.WriteLine($"{heading}\nAwesome let's change to a different account!\n\n");
                        Console.WriteLine("Would you like to:\n\n");
                        Console.WriteLine("C -> Change to checking account? ");
                        Console.WriteLine("S -> Change to savings account? ");
                        Console.WriteLine("L -> Change to loan account? ");
                        Console.WriteLine("T -> Change to credit line account? ");
                        Console.WriteLine("B -> Go Back \n\n");
                        Console.Write("Enter choice: ");
                        changeChoice = Console.ReadLine();
                    }

                    switch (changeChoice)
                    {
                        case "C":
                            currentAccount = "Checking Account";
                            userMenu();
                            break;
                        case "S":
                            if (currentUser.checkForSavingsAccount())
                            {
                                currentAccount = "Savings Account";
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine($"{heading}\nYou need to create a Savings Account!\n\n");
                                Console.Write("Press <Enter> to go back to menu...");
                                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                            }
                            userMenu();
                            break;
                        case "L":
                            Console.Clear();
                            Console.WriteLine($"{heading}\nLoan accounts have not been setup yet, please check back soon!\n\n");
                            Console.Write("Press <Enter> to go back to menu...");
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                            userMenu();
                            break;
                        case "T":
                            Console.Clear();
                            Console.WriteLine($"{heading}\nCredit line accounts have not been setup yet, please check back soon!\n\n");
                            Console.Write("Press <Enter> to go back to menu...");
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                            userMenu();
                            break;
                        case "B":
                            userMenu();
                            break;
                    }

                    JsonSave(); //saving to file



                    break;
                //Create a new account
                case "A":
                    string accountType = "";
                    string[] accountTypes = { "S", "L", "C", "B" };

                    while (!accountTypes.Contains(accountType)) //LINQ query operator
                    {
                        Console.Clear();
                        Console.WriteLine($"{heading}\nAwesome let's create a new account!\n\n");
                        Console.WriteLine("Would you like to:\n\n");
                        Console.WriteLine("S -> Create a new savings account? ");
                        Console.WriteLine("L -> Create a new loan account? ");
                        Console.WriteLine("C -> Create a new credit line account? ");
                        Console.WriteLine("B -> Go Back \n\n");
                        Console.Write("Enter choice: ");
                        accountType = Console.ReadLine();
                    }

                    switch (accountType)
                    {
                        case "S":
                            Console.Clear();
                            if (!currentUser.checkForSavingsAccount())
                            {
                                currentUser.createNewAccount("Savings Account");
                                Console.WriteLine($"{heading}\nCongratulations you have setup a new Savings Account!\n\n");
                            }
                            else
                            {
                                Console.WriteLine($"{heading}\nYou have already created a Savings Account!\n\n");
                            }
                            Console.Write("Press <Enter> to go back to menu...");
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                            userMenu();
                            break;
                        case "L":
                            Console.Clear();
                            Console.WriteLine($"{heading}\nLoan accounts have not been setup yet, please check back soon!\n\n");
                            Console.Write("Press <Enter> to go back to menu...");
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                            userMenu();
                            break;
                        case "C":
                            Console.Clear();
                            Console.WriteLine($"{heading}\nCredit line accounts have not been setup yet, please check back soon!\n\n");
                            Console.Write("Press <Enter> to go back to menu...");
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                            userMenu();
                            break;
                        case "B":
                            userMenu();
                            break;
                    }

                    break;
                //Deposit into Current Account
                case "D":
                    string description = "";
                    double amount = 0.0;

                    Console.Clear();
                    Console.WriteLine($"{heading}\nAwesome let's make a deposit into your {currentAccount}!\n\n");
                    while (description.Equals(""))
                    {
                        Console.Write("Enter Deposit Description: ");
                        description = Console.ReadLine();
                        if (description.Equals(""))
                        {
                            Console.WriteLine("\nPlease enter a description!\n");
                        }
                    }
                    while (amount <= 0.0)
                    {
                        Console.Write("Enter Deposit Amount: $");
                        try
                        {
                            amount = double.Parse(Console.ReadLine());
                            if (amount <= 0.0)
                            {
                                Console.WriteLine("\nPlease enter a positive number!\n");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("\nPlease enter a valid number!\n");
                        }
                        
                        
                    }
                    currentUser.deposit(currentAccount, description, amount);

                    Console.WriteLine($"\n\nCongratulations, You Deposited $" + string.Format("{0:0.00}", amount)+ $" into your {currentAccount}\n");
                    Console.WriteLine($"Your current balance is now $" + string.Format("{0:0.00}", currentUser.getBalance(currentAccount)) + "\n\n");
                    Console.Write("Press <Enter> to go back to menu...");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    userMenu();

                    break;
                //Withdraw from Current Account
                case "W":
                    string withdrawDescription = "";
                    double withdrawAmount = 0.0;

                    Console.Clear();
                    Console.WriteLine($"{heading}\nAwesome let's make a withdraw from your {currentAccount}!\n\n");

                    while (withdrawDescription.Equals("") || withdrawAmount <= 0.0 || withdrawAmount > currentUser.getBalance(currentAccount))
                    {
                        
                        Console.Write("Enter Withdraw Description: ");
                        withdrawDescription = Console.ReadLine();
                        if (!withdrawDescription.Equals(""))
                        {

                            Console.Write("Enter Withdraw Amount: $");

                            try
                            {
                                withdrawAmount = double.Parse(Console.ReadLine());
                                if (withdrawAmount <= 0.0)
                                {
                                    Console.WriteLine("\nPlease enter a valid positive number!\n");
                                }
                                if (currentUser.getBalance(currentAccount) < withdrawAmount)
                                {
                                    Console.WriteLine($"\nYour funds are lacking!! You currently have $" + string.Format("{0:0.00}", currentUser.getBalance(currentAccount)) + $" but requested $" + string.Format("{0:0.00}", withdrawAmount) + "\n\n");
                                    Console.WriteLine("Please try again!");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("\nPlease enter valid number!\n");
                            }

                        }

                    }

                    currentUser.withdraw(currentAccount, withdrawDescription, withdrawAmount);
                    Console.WriteLine($"\n\nCongratulations, You Withdrew $" + string.Format("{0:0.00}", withdrawAmount)+ $" from your {currentAccount}\n");
                    Console.WriteLine($"Your current balance is now $" + string.Format("{0:0.00}", currentUser.getBalance(currentAccount)) + "\n\n");
                    Console.Write("Press <Enter> to go back to menu...");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    userMenu();
                    break;
                //View All Transactions from current account
                case "V":
                    Console.Clear();
                    Console.WriteLine($"{heading}\nLet's view all transactions in your {currentAccount}!\n\n");

                    currentUser.listTransactions(currentAccount);

                    Console.Write("\n\nPress <Enter> to go back to menu...");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    userMenu();
                    break;
                //Check balance of current account
                case "B":
                    Console.Clear();
                    Console.WriteLine($"{heading}\nLet's check the balance of your {currentAccount}!\n\n");

                    double balance = currentUser.getBalance(currentAccount);
                    Console.WriteLine($"Your current balance of your {currentAccount} is $" + string.Format("{0:0.00}", balance) + "!");

                    Console.Write("\n\nPress <Enter> to go back to menu...");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    userMenu();

                    break;
                //Log out back to main page
                case "L":
                    run();
                    break;
            }

        }

    }
}
