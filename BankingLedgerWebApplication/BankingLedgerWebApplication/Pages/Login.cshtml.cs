using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankingLedgerWebApplication.Pages
{
    public class ContactModel : PageModel
    {

        public string Message { get; set; }

        public void OnGet()
        {
            
            Message = "Your contact page.";
        }

        public void OnPost()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            Session session;
            session = Program.getSession();
            session.JsonRead();
            bool loginSuccess = session.login(username, password);

            if (loginSuccess)
            {
                TempData["username"] = username;
                TempData["password"] = password;
                //TempData["session"] = session;
            }
            
        }

    }
}
