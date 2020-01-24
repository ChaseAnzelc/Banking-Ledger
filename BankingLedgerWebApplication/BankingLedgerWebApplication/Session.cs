using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BankingLedgerWebApplication
{
    public class Session
    {
        private Client currentUser = new Client(); //Keeps track of current user
        public string currentAccount; //Keeps track of current account
        private List<Client> clientList; //Holds all clients
        public string heading = "The Best Banking Ledger Ever!\n\n";
        //public string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\")) + "database.json"; //Path to Json file "database"
        public string path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\database.json");

        public List<Transaction> viewAllTransactions()
        {
            return currentUser.listTransactions(currentAccount);
        }

        public double getBalance(string currentaccount)
        {
            return currentUser.getBalance(currentaccount);
        } 

        public void withdraw(string currentaccount, string description, double amount)
        {
            currentUser.withdraw(currentaccount, description, amount);

        }

        public void deposit(string currentaccount, string description, double amount)
        {
            currentUser.deposit(currentaccount, description, amount);
            
        }

        public string CurrentUserFullName()
        {
            return $"{currentUser.firstName} {currentUser.lastName}";
        }

        
        public bool IsUserSet()
        {
            if (!currentUser.firstName.Equals(""))
            {
                return true;
            }

            return false;
        }

        public void JsonSave()
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

        //Logging into Account
        public bool login(string userName, string password)
        {
            int i = 0;
            bool set = false;

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
                return false;
            }
            else
            {
                return true;
                
            }

        }

        public void createAccount(string firstName, string lastName, string userName, string password)
        {
            //Creating current user
            currentUser = new Client(firstName, lastName, userName, password);
            currentUser.createNewAccount("Checking Account");
            currentUser.createNewAccount("Savings Account");

            if (!File.Exists(path))
            {
                //Adding user to a List of Customers [Will write the List to json file on close]
                clientList = new List<Client>();
            }
            clientList.Add(currentUser);

        }

    }
}
