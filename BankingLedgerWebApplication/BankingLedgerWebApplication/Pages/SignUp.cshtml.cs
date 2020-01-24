using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankingLedgerWebApplication.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }

        public void OnPost()
        {

            string firstname = Request.Form["firstname"];
            string lastname = Request.Form["lastname"];
            string username = Request.Form["username"];
            string password = Request.Form["password"];


            Session session;
            session = Program.getSession();


            session.createAccount(firstname, lastname, username, password);

            session.JsonSave();

        }

    }
}
