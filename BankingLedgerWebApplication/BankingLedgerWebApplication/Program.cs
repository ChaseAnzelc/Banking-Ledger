using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BankingLedgerWebApplication
{
    public class Program
    {
        static Session session { get; set; }


        public static Session resetSession()
        {
            session = new Session();
            session.JsonRead();

            return session;
        }

        public static Session getSession()
        {

            return session; 
        }

        public static void Main(string[] args)
        {
            session = new Session();
            session.JsonRead();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        
    }
}
