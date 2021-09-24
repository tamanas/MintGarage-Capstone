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
            var partners = partnerRepository.Partner;
            return View(partners);
        }

        public IActionResult Create()
        {
            return RedirectToAction("Update");
        }

        public IActionResult Edit(Partner? partner, int? id)
        {
            return RedirectToAction("Update");
        }

        public IActionResult Delete(Partner? partner, int? id)
        {
            return RedirectToAction("Update");
        }
    }
}
