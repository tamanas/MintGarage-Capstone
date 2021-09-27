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
            return RedirectToAction("Update");
        }*/

        public IActionResult Update(int? id, string? operation, bool? show)
        {
            ViewBag.add = false;
            ViewBag.edit = false;
            ViewBag.delete = false;
            
            if(operation != null && show != null)
            {
                switch (operation)
                {
                    case "add":
                        ViewBag.add = show;
                        break;
                    case "edit":
                        ViewBag.edit = show;
                        break;
                    case "delete":
                        ViewBag.delete = show;
                        break;
                }
            }

            PartnerUpdateView partnerUpdateView = new PartnerUpdateView();
            partnerUpdateView.Partners = partnerRepository.Partners;
            if (id != null && operation != "add")
            {
                partnerUpdateView.Partner = partnerRepository.Partners.FirstOrDefault(s => s.PartnerID == id); ;
            }
            return View(partnerUpdateView);
        }


        public IActionResult Create(Partner? partner)
        {
            partnerRepository.Create(partner);
            return RedirectToAction("Update");
        }

        public IActionResult Edit(PartnerUpdateView partnerUpdateView)
        {
            partnerRepository.Edit(partnerUpdateView.Partner);
            return RedirectToAction("Update");
        }

        public IActionResult Delete(PartnerUpdateView partnerUpdateView)
        {
            partnerRepository.Delete(partnerUpdateView.Partner);
            return RedirectToAction("Update");
        }

    }
}
