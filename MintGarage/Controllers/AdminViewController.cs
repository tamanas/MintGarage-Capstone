using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Controllers
{
    public class AdminViewController : Controller
    {

        public IActionResult Index()
        {
            HttpContext.Session.Set("isAdminLoggedIn", BitConverter.GetBytes(true));
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Set("isAdminLoggedIn", BitConverter.GetBytes(false));
            return Redirect("../Home/Index");
        }
    }
}
