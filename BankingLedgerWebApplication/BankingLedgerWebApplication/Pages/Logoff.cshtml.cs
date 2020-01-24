using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankingLedgerWebApplication.Pages
{
    public class LogoffModel : PageModel
    {

        public string Message { get; set; }

        public void OnGet()
        {
            
            Message = "Your contact page.";
        }

        public void OnPost()
        {
           
            
        }

    }
}
