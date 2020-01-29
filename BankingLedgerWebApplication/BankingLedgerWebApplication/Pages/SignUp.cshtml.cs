using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
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
            //Also validated on the front-end with JQuery
            //Double check here to make sure input is indeed valid


            TempData["firstname"] = false;
            TempData["lastname"] = false;
            TempData["username"] = false;
            TempData["password"] = false;

            string namePattern = "^[A-Z][a-z]+$";
            string usernamePattern = "^[A-Za-z0-9_-]{3,15}$";
            string passwordPattern = "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$";


            string firstname = Request.Form["firstname"];
            string lastname = Request.Form["lastname"];
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            //checking input against REGEX
            Match m = Regex.Match(firstname, namePattern);
            if (m.Success) { TempData["firstname"] = true; }
            m = Regex.Match(lastname, namePattern);
            if (m.Success) { TempData["lastname"] = true; }
            m = Regex.Match(username, usernamePattern);
            if (m.Success) { TempData["username"] = true; }
            m = Regex.Match(password, passwordPattern);
            if (m.Success) { TempData["password"] = true; }

            //only add user if input is checked a
            if ((bool)TempData["firstname"] && (bool)TempData["lastname"] && (bool)TempData["username"] && (bool)TempData["password"])
            {
                Session session;
                session = Program.getSession();


                session.createAccount(firstname, lastname, username, password);

                session.JsonSave();
            }

        }

    }
}
