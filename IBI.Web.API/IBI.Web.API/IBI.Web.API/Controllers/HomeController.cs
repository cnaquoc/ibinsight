using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IBI.Data.API;

namespace IBI.Web.API.Controllers
{
    public class HomeController : Controller
    {
        
        public HomeController()
        {
            
        }
        public IActionResult Index()
        {
            //Add new company  
            
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
