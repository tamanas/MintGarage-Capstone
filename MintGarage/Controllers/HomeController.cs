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
        public IPartnerRepository partnerRepository;

        public HomeController(IPartnerRepository partnerRepo) { 
            partnerRepository = partnerRepo;
        }

        public IActionResult Index()
        {
            ViewBag.Partners = partnerRepository.Partners;
            return View();
        }

        public IActionResult Update()
        {
            ViewBag.Partners = partnerRepository.Partners;
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.Partners = partnerRepository.Partners;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.Partners = partnerRepository.Partners;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
