using Microsoft.AspNetCore.Mvc;
using MintGarage.Models.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace MintGarage.Controllers
{
    public class PartnersController : Controller
    {

        public IPartnerRepository partnerRepository;

        public PartnersController(IPartnerRepository partnerRepo)
        {
            partnerRepository = partnerRepo;
        }

        public IActionResult Index()
        {
            ViewData["PartnersArray"] = "test";
            
            return View("_Layout");
        }

        public IActionResult Update()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
