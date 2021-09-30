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

      /*  public IActionResult Index()
        {
            ViewData["PartnersArray"] = "test";
            
            return View("_Layout");
        }

            PartnerUpdateView partnerUpdateView = new PartnerUpdateView();
            partnerUpdateView.Partners = partnerRepository.Partners;
            if (id != null && operation != "add")
            {
                partnerUpdateView.Partner = partnerRepository.Partners.FirstOrDefault(s => s.PartnerID == id); ;
            }
            return View(partnerUpdateView);
        }


        public IActionResult Create(PartnerUpdateView partnerUpdateView)
        {
            if (ModelState.IsValid)
            {
                partnerRepository.Create(partnerUpdateView.Partner);
                TempData["AdminPartnerMessage"] = "Successfully added new Partner.";
            } else
            {
                partnerUpdateView.Partners = partnerRepository.Partners;
                setViewBag(true, false, false);
                return View("Update", partnerUpdateView);
            }
            return RedirectToAction("Update");
        }

        public IActionResult Edit(PartnerUpdateView partnerUpdateView)
        {
            if (ModelState.IsValid)
            {
                partnerRepository.Edit(partnerUpdateView.Partner);
                TempData["AdminPartnerMessage"] = "Successfully edited Partner.";
            }
            else
            {
                partnerUpdateView.Partners = partnerRepository.Partners;
                setViewBag(false, true, false);
                return View("Update", partnerUpdateView);
            }
            return RedirectToAction("Update");
        }

        public IActionResult Delete(PartnerUpdateView partnerUpdateView)
        {
            partnerRepository.Delete(partnerUpdateView.Partner);
            TempData["AdminPartnerMessage"] = "Successfully deleted Partner.";
            return RedirectToAction("Update");
        }

        public void setViewBag(bool add, bool edit, bool delete)
        {
            ViewBag.add = add;
            ViewBag.edit = edit;
            ViewBag.delete = delete;
        }

    }
}
