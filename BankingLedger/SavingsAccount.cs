using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace BankingLedger
{
    public class SavingsAccount : Account // Child Class
    {
        public SavingsAccount()
        {

        }


        public SavingsAccount(string accountIdentifier, int accountNumber, double balance) : base(accountIdentifier, accountNumber, balance)
        {
           
        }

        public override void deposit(string description, double depositAmount)
        {
            //create transaction
            Transaction transaction = new Transaction("Deposit", description, DateTime.Now.ToString("M-dd-yyyy/HH:mm:ss"), depositAmount);

            //add transactions to savings account List
            AccountTransactions.Add(transaction);
            balance += Math.Round(depositAmount, 2);

        }

        public override void withdraw(string description, double withdrawAmount)
        {
            //create transaction
            Transaction transaction = new Transaction("Withdraw", description, DateTime.Now.ToString("M-dd-yyyy/HH:mm:ss"), withdrawAmount);

            //add transactions to savings account List
            AccountTransactions.Add(transaction);
            balance -= Math.Round(withdrawAmount, 2);
        }

        

    }
}
