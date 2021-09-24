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

        public IActionResult Update(int? id)
        {
            PartnerUpdateView partnerUpdateView = new PartnerUpdateView();
            partnerUpdateView.Partners = partnerRepository.Partners;
            ViewBag.add = true;
            ViewBag.delete = true;

            if (id != null)
            {
                ViewBag.edit = true;
                partnerUpdateView.Partner = partnerRepository.Partners.FirstOrDefault(s => s.PartnerID == id); ;
            }
            else
            {
                ViewBag.edit = false;
            }
            return View(partnerUpdateView);
        }

        public IActionResult Create(Partner? partner)
        {
            //ViewBag.add = true;
         /*   if (partner is not null)
            {
                ViewBag.add = true;*/
                partnerRepository.Create(partner);

         /*   }
            else
            {
                ViewBag.add = false;
            }*/
            return RedirectToAction("Update");
        }

        [HttpPost]
        public IActionResult Edit(PartnerUpdateView partnerUpdateView)
        {
            partnerRepository.Edit(partnerUpdateView.Partner);
            return RedirectToAction("Update");
        }

        /* public IActionResult Delete(int? id)
         {
             var partner = partnerRepository.Partner.FirstOrDefault(s => s.PartnerID == id);
 *//*            partnerRepository.Delete(partner);
 *//*            return RedirectToAction("Update", partner);
         }*/

        [HttpPost]
        public IActionResult Delete(PartnerUpdateView partnerUpdateView)
        {
            partnerRepository.Delete(partnerUpdateView.Partner);
            return RedirectToAction("Update");
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Update");
        }

    }
}
