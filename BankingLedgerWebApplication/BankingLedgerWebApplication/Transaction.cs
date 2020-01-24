using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingLedgerWebApplication
{
    public class Transaction 
    {
        [JsonProperty("transactionType")]
        public string transactionType { get; private set; }
        [JsonProperty("description")]
        public string description { get; private set; }
        [JsonProperty("dateTime")]
        public string dateTime { get; private set; }
        [JsonProperty("amount")]
        public double amount { get; private set; }


        public Transaction() { }

        public Transaction(string transactionType, string description, string dateTime, double amount)
        {
            this.transactionType = transactionType;
            this.description = description;
            this.dateTime = dateTime;
            this.amount = amount;
        }



    }
}
