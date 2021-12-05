using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using MintGarage.Models.FooterT.SocialMedias;
using MintGarage.Models.FooterT.ContactInformation;
using MintGarage.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MintGarage.Models.PartnerT;

namespace MintGarage.Controllers
{
    public class FooterController : Controller
    {
        private IRepository<ContactInfo> contactInfoRepo;
        private IRepository<SocialMedia> socialMediaRepo;
        public IRepository<Partner> partnerRepo;
        private IWebHostEnvironment hostEnv;

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
        "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";

        public FooterController(IRepository<ContactInfo> contactRepo,
            IRepository<SocialMedia> mediaRepo, IWebHostEnvironment hostEnvironment,
            IRepository<Partner> partnerRepository)
        {
            contactInfoRepo = contactRepo;
            socialMediaRepo = mediaRepo;
            partnerRepo = partnerRepository;
            hostEnv = hostEnvironment;
        }

        public IActionResult Update(int? id, string? operation, bool? show)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            ViewBag.message = TempData["message"];

            ViewBag.contactInfoMessage = TempData["AdminFooterContactInfoMessage"];
            ViewBag.socialMediaMessage = TempData["AdminFooterSocialMediaMessage"];
            SetViewBag(false, false, false, false);

            if (operation != null && show != null)
            {
                switch (operation)
                {
                    case "contactEdit":
                        ViewBag.contactEdit = show;
                        break;
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

            FooterModel footerModel = new FooterModel();
            footerModel.ContactInfos = contactInfoRepo.Items;
            footerModel.SocialMedias = socialMediaRepo.Items;
            if (id != null && operation != "add")
            {
                footerModel.ContactInfo = contactInfoRepo.Items.FirstOrDefault(s => s.ContactInfoID == id);
                footerModel.SocialMedia = socialMediaRepo.Items.FirstOrDefault(s => s.SocialMediaID == id);
            }

            return View(footerModel);
        }

        public IActionResult EditFooterContactInfo(FooterModel footerModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
           
            if (ModelState.IsValid)
            {
                contactInfoRepo.Update(footerModel.ContactInfo);
                TempData["message"] = "Successfully edited Contact Info.";
            }
            else
            {
                footerModel.ContactInfos = contactInfoRepo.Items;
                footerModel.SocialMedias = socialMediaRepo.Items;
                SetViewBag(true, false, false, false);
                return View("Update", footerModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> AddSocialMedia(FooterModel footerModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                socialMediaRepo.Create(footerModel.SocialMedia);
                TempData["message"] = "Successfully added Social Media.";
            }
            else
            {
  
                footerModel.ContactInfos = contactInfoRepo.Items;
                footerModel.SocialMedias = socialMediaRepo.Items;
                SetViewBag(false, true, false, false);
                return View("Update", footerModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> EditSocialMedia(FooterModel footerModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                socialMediaRepo.Update(footerModel.SocialMedia);
                TempData["message"] = "Successfully edited Social Media.";
            }
            else
            {
                footerModel.ContactInfos = contactInfoRepo.Items;
                footerModel.SocialMedias = socialMediaRepo.Items;
                SetViewBag(false, false, true, false);
                return View("Update", footerModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult DeleteSocialMedia(FooterModel footerModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            socialMediaRepo.Delete(footerModel.SocialMedia);
            TempData["message"] = "Successfully deleted Social Media.";
            return RedirectToAction("Update");
        }

        public void SetViewBag(bool contactEdit, bool add, bool edit, bool delete)
        {
            ViewBag.contactEdit = contactEdit;
            ViewBag.add = add;
            ViewBag.edit = edit;
            ViewBag.delete = delete;
        }
    }
}

