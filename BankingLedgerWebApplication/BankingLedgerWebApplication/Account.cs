using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BankingLedgerWebApplication
{
    public abstract class Account //Parent - Class - [Current children are Checking Account and Savings Account - more to come later]
    {
        [JsonProperty("AccountTransactions")]
        public List<Transaction> AccountTransactions;
        [JsonProperty("accountIdentifier")]
        public string accountIdentifier { get; private set; }
        [JsonProperty("accountNumber")]
        public int accountNumber { get; private set; }
        [JsonProperty("balance")]
        public double balance { get; set; }

        
        public Account()
        {

        }

        public Account(string accountIdentifier, int accountNumber, double balance)
        {
            this.accountIdentifier = accountIdentifier;
            this.accountNumber = accountNumber;
            this.balance = balance;
            AccountTransactions = new List<Transaction>();
        }

        public virtual void deposit(string description, double depositAmount)
        {
            Console.WriteLine("Deposit in Parent Class - Please Override");
        }

        public virtual void withdraw(string decription, double withdrawamount)
        {
            Console.WriteLine("Withdraw in Parent Class - Please Override");
        }




    }
}
