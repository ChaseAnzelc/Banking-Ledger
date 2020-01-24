using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankingLedgerWebApplication
{
    public class DepositModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            
            string description = Request.Form["description"];
            double amount = double.Parse(Request.Form["amount"]);

            Session session;
            session = Program.getSession();


            session.deposit(session.currentAccount,description, amount);
            session.JsonSave();

            TempData["curacct"] = "Changed";

        }
    }
}