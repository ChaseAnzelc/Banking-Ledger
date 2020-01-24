using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankingLedgerWebApplication
{
    public class WithdrawModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            string currentaccount = (string)TempData["currentaccount"];
            string description = Request.Form["description"];
            double amount = double.Parse(Request.Form["amount"]);

            Session session;
            session = Program.getSession();

            session.withdraw(currentaccount, description, amount);
            session.JsonSave();

            TempData["curacct"] = "Changed";
        }
    }
}