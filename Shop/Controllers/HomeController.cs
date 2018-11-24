using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.PayPal;
using Microsoft.AspNetCore.Http;

namespace Shop.Controllers
{    
    public class HomeController : Controller
    {
        string str = "Val-" + Ret();
        
        public static int Ret()
        {
            return 5;
        }


        [HttpPost]
        public ActionResult CreatePurchase(string nonce)
        {
            
            // Use payment method nonce here
            return null;
        }
        public IActionResult Index()
        {
            //new X();
            //var x = new ClientTokenHandler();
            //object o = x;
            //ClientTokenHandler t = o;
            //var s1 = string.Intern("Hello");
            //var s2 = string.Intern("Hello");
            //var eq = ReferenceEquals(s1, s2);
            //s1 += "1";
            //var eq2 = ReferenceEquals(s1, s2);
            //int i = 1;
            //s1 = $"i = {++i}";

            //var x = new { W ="qq", k = "q"};
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

       

        
    }
}
