using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SeeCali
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       /* public String Index()
        {
            return "Hello, ASP.NET Core MVC!";
        }*/

    }
}