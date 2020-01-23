using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create new session
            Session session = new Session();
            //Read from Json file "database" if it exists
            session.JsonRead();
            //Start session
            session.run();
        }
    }
}
