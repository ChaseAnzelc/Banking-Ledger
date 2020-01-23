using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingLedger
{
    public abstract class Person //Parent-Class - [Current child is Client class - setup for more children classes later]
    {

        //Encapsulation- Making sure that "sensitive" data is hidden from users with private setters
        [JsonProperty("firstName")]
        public string firstName { get; private set; }
        [JsonProperty("lastName")]
        public string lastName { get; private set; }
        [JsonProperty("userName")]
        public string userName { get; private set; }
        [JsonProperty("password")]
        public string password { get; private set; }



        public Person() { }

        public Person(string firstName, string lastName, string userName, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.userName = userName;
            this.password = password;
        }

    }
}
