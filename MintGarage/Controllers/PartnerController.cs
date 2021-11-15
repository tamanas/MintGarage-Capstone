using Microsoft.AspNetCore.Mvc;
using MintGarage.Models.PartnerT;
using System;
using System.Linq;
using MintGarage.Models.FooterT.SocialMedias;
using MintGarage.Models.FooterT.ContactInformation;
using MintGarage.Models;

namespace MintGarage.Controllers
{
    public class PartnerController : Controller
    {

        public IRepository<Partner> partnerRepo;
        private IRepository<ContactInfo> contactInfoRepo;
        private IRepository<SocialMedia> socialMediaRepo;

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
        "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";

        public PartnerController(IRepository<Partner> partnerRepository, 
            IRepository<ContactInfo> contactRepo, IRepository<SocialMedia> mediaRepo)
        {
            partnerRepo = partnerRepository;
            contactInfoRepo = contactRepo;
            socialMediaRepo = mediaRepo;
        }

        public IActionResult Update(int? id, string? operation, bool? show)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            SetViewBag(false, false, false);
            ViewBag.message = TempData["AdminPartnerMessage"];

            if (operation != null && show != null)
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

            PartnerModel partnerUpdateView = new PartnerModel();
            partnerUpdateView.Partners = partnerRepo.Items;
            if (id != null && operation != "add")
            {
                partnerUpdateView.Partner = partnerRepo.Items.FirstOrDefault(s => s.PartnerID == id); ;
            }
            return View(partnerUpdateView);
        }

        public IActionResult Create(PartnerModel partnerModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                partnerRepo.Create(partnerModel.Partner);
                TempData["AdminPartnerMessage"] = "Successfully added new Partner.";
            } else
            {
                partnerModel.Partners = partnerRepo.Items;
                SetViewBag(true, false, false);
                return View("Update", partnerModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult Edit(PartnerModel partnerModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                partnerRepo.Update(partnerModel.Partner);
                TempData["AdminPartnerMessage"] = "Successfully edited Partner.";
            }
            else
            {
                partnerModel.Partners = partnerRepo.Items;
                SetViewBag(false, true, false);
                return View("Update", partnerModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult Delete(PartnerModel partnerModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            partnerRepo.Delete(partnerModel.Partner);
            TempData["AdminPartnerMessage"] = "Successfully deleted Partner.";
            return RedirectToAction("Update");
        }

        public void SetViewBag(bool add, bool edit, bool delete)
        {
            ViewBag.add = add;
            ViewBag.edit = edit;
            ViewBag.delete = delete;
        }

    }
}
