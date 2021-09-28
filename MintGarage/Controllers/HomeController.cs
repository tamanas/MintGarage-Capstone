using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MintGarage.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UpdateContactUs()
        {
            return View();
        }

        public IActionResult UpdateSocial()
        {
            return View();
        }

        public IActionResult UpdateReviews()
        {
            return View();
        }

        public IActionResult UpdateSuppliers()
        {
            return View();
        }

        public IActionResult UpdateHome()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}





