using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Controllers
{
    public class AdminViewController : Controller
    {
        bool isAdminLoggedIn = false;

        public IActionResult Index()
        {
            isAdminLoggedIn = true;
            return View();
        }

        public IActionResult LogOut()
        {
            isAdminLoggedIn = false;
            return View("../Home/Index");
        }
    }
}
