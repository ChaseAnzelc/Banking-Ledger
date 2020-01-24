using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace BankingLedgerWebApplication
{
    class Client : Person //Child-Class - [More Child classes could be Teller, Manager, Accountant..etc]
    {
        [JsonProperty("Checking Account")]
        CheckingAccount checkingAccount { get; set; }
        [JsonProperty("Savings Account")]
        SavingsAccount savingsAccount { get; set; }

        public Client()
        {
            
        }

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

        public List<Transaction> listTransactions(string accountType)
        {
            Console.WriteLine($"List of Transactions: \n");
            int i = 0;
            List<Transaction> transactionList = new List<Transaction>();


            if (accountType.Equals("Checking Account")) 
            {
                return checkingAccount.AccountTransactions;
                //foreach (var trans in checkingAccount.AccountTransactions)
                //    {
                        //i++;
                       //return $"Type: {trans.transactionType} Description: {trans.description} Date/Time: {trans.dateTime} Amount: ${trans.amount}";
                    
                //}
            }
            else if(accountType.Equals("Savings Account"))
            {
                return savingsAccount.AccountTransactions;
                
                //foreach (var trans in savingsAccount.AccountTransactions)
                //{
                    //i++;
                    //return $"Type: {trans.transactionType} Description: {trans.description} Date/Time: {trans.dateTime} Amount: ${trans.amount}";
                //}
            }

            if (i == 0) { return transactionList; }

            return transactionList;

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
