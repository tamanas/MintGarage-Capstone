using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MintGarage.Models;
using MintGarage.Models.Partners;
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
        public IPartnerRepository partnerRepository;

        public HomeController(ILogger<HomeController> logger, IPartnerRepository partnerRepo)
        {
            _logger = logger;
            partnerRepository = partnerRepo;
        }

        public IActionResult Index()
        {
            return View(partnerRepository.Partners);
        }

        public IActionResult Update()
        {
            return View();
        }

        public IActionResult Privacy()
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
