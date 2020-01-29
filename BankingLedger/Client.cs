using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace BankingLedger
{
    class Client : Person //Child-Class - [More Child classes could be Teller, Manager, Accountant..etc]
    {
        [JsonProperty("Checking Account")]
        CheckingAccount checkingAccount { get; set; }
        [JsonProperty("Savings Account")]
        SavingsAccount savingsAccount { get; set; }

        public Client(string firstName, string lastName, string userName, string password) : base(firstName, lastName, userName, password)
        {
            savingsAccount = new SavingsAccount();
        }

        public void createNewAccount(string accountType)
        {
            if (accountType.Equals("Checking Account"))
            {
                checkingAccount = new CheckingAccount(accountType, 1001, 0);
            }
            else if (accountType.Equals("Savings Account"))
            {
                savingsAccount = new SavingsAccount(accountType, checkingAccount.accountNumber + 1, 0);
            } 
        }

        public double getBalance(string accountType)
        {
            double balance = 0;
            if(accountType.Equals("Checking Account"))
            {
                balance = checkingAccount.balance;
            }
            else if(accountType.Equals("Savings Account"))
            {
                balance = savingsAccount.balance;
            }


            return balance;
        }

        public void listTransactions(string accountType)
        {
            Console.WriteLine($"List of Transactions: \n");
            int i = 0;

            if (accountType.Equals("Checking Account")) 
            {
                foreach (var trans in checkingAccount.AccountTransactions)
                    {
                        i++;
                        Console.WriteLine($"Type: {trans.transactionType} Description: {trans.description} Date/Time: {trans.dateTime} Amount: $" + string.Format("{0:0.00}", trans.amount));
                    }
            }
            else if(accountType.Equals("Savings Account"))
            {
                foreach (var trans in savingsAccount.AccountTransactions)
                {
                    i++;
                    Console.WriteLine($"Type: {trans.transactionType} Description: {trans.description} Date/Time: {trans.dateTime} Amount: $" + string.Format("{0:0.00}", trans.amount));
                }
            }

            if (i == 0) { Console.WriteLine("You don't have any transactions yet!"); }

        }

        public void deposit(string accountType, string description, double depositAmount)
        {

            if (accountType.Equals("Checking Account"))
            {
                checkingAccount.deposit(description, depositAmount);
            }
            else if (accountType.Equals("Savings Account"))
            {
                savingsAccount.deposit(description, depositAmount);
            }
            
         
        }

        public void withdraw(string accountType, string description, double withdrawAmount)
        {
            if (accountType.Equals("Checking Account"))
            {
                checkingAccount.withdraw(description, withdrawAmount);
            }
            else if (accountType.Equals("Savings Account"))
            {
                savingsAccount.withdraw(description, withdrawAmount);
            }

        }

        public bool checkForSavingsAccount()
        {
            return (savingsAccount.accountNumber != 0);
        }


    }
}
