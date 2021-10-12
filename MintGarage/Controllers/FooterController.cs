using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using MintGarage.Models.FooterContents.FooterContactInfo;
using MintGarage.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MintGarage.Models.Partners;

namespace MintGarage.Controllers
{
    public class FooterController : Controller
    {

        private IFooterContactInfoRepository footerContactInfoRepository;
        private IFooterSocialMediaRepository footerSocialMediaRepository;
        public IPartnerRepository partnerRepository;
        private IWebHostEnvironment hostEnv;
        private string imageFolder = "/Images/";

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
        "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";

        public FooterController(IFooterContactInfoRepository footerContactInfoRepo,
            IFooterSocialMediaRepository footerSocialMediaRepo, IWebHostEnvironment hostEnvironment,
            IPartnerRepository partnerRepo)
        {
            footerContactInfoRepository = footerContactInfoRepo;
            footerSocialMediaRepository = footerSocialMediaRepo;
            partnerRepository = partnerRepo;
            hostEnv = hostEnvironment;
        }

        public IActionResult Update(int? id, string? operation, bool? show)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

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
            footerModel.FooterContactInfo = footerContactInfoRepository.FooterContactInfo;
            footerModel.FooterSocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            if (id != null && operation != "add")
            {
                footerModel.FooterContact = footerContactInfoRepository.FooterContactInfo.FirstOrDefault(s => s.FooterContactInfoID == id);
                footerModel.FooterSocialMedia = footerSocialMediaRepository.FooterSocialMedias.FirstOrDefault(s => s.FooterSocialMediaID == id);
            }

            return View(footerModel);
        }

        public IActionResult EditFooterContactInfo(FooterModel footerModel)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
           
            if (ModelState.IsValid)
            {
                footerContactInfoRepository.Update(footerModel.FooterContact);
                TempData["AdminFooterContactInfoMessage"] = "Successfully edited Contact Info.";
            }
            else
            {
                footerModel.FooterContactInfo = footerContactInfoRepository.FooterContactInfo;
                footerModel.FooterSocialMedias = footerSocialMediaRepository.FooterSocialMedias;
                SetViewBag(true, false, false, false);
                return View("Update", footerModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> AddSocialMedia(FooterModel footerModel)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid && footerModel.FooterSocialMedia.ImageFile != null)
            {
                footerModel.FooterSocialMedia.SocialMediaLogo = await SaveImage(footerModel.FooterSocialMedia.ImageFile);
                footerSocialMediaRepository.Create(footerModel.FooterSocialMedia);
            }
            else
            {
                if (footerModel.FooterSocialMedia.ImageFile == null)
                {
                    ModelState.AddModelError("image", "Image is required");
                }
                footerModel.FooterContactInfo = footerContactInfoRepository.FooterContactInfo;
                footerModel.FooterSocialMedias = footerSocialMediaRepository.FooterSocialMedias;
                SetViewBag(false, true, false, false);
                return View("Update", footerModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> EditSocialMedia(FooterModel footerModel)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                if (footerModel.FooterSocialMedia.ImageFile != null)
                {
                    DeleteImage(footerModel.FooterSocialMedia.SocialMediaLogo);
                    footerModel.FooterSocialMedia.SocialMediaLogo = await SaveImage(footerModel.FooterSocialMedia.ImageFile);
                }
                footerSocialMediaRepository.Edit(footerModel.FooterSocialMedia);
                TempData["AdminFooterSocialMediaMessage"] = "Successfully edited Social Media.";
            }
            else
            {
                footerModel.FooterContactInfo = footerContactInfoRepository.FooterContactInfo;
                footerModel.FooterSocialMedias = footerSocialMediaRepository.FooterSocialMedias;
                SetViewBag(false, false, true, false);
                return View("Update", footerModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult DeleteSocialMedia(FooterModel footerModel)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            DeleteImage(footerModel.FooterSocialMedia.SocialMediaLogo);
            footerSocialMediaRepository.Delete(footerModel.FooterSocialMedia);
            TempData["AdminFooterSocialMediaMessage"] = "Successfully deleted Social Media.";
            return RedirectToAction("Update");
        }

        public void SetViewBag(bool contactEdit, bool add, bool edit, bool delete)
        {
            ViewBag.contactEdit = contactEdit;
            ViewBag.add = add;
            ViewBag.edit = edit;
            ViewBag.delete = delete;
        }

        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = Path.GetFileNameWithoutExtension(imageFile.FileName) +
                                        DateTime.Now.ToString("yyMMddssffff") +
                                        Path.GetExtension(imageFile.FileName);
            string imagePath = Path.Combine(hostEnv.WebRootPath + imageFolder, imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        public void DeleteImage(string imageName)
        {
            string imagePath = Path.Combine(hostEnv.WebRootPath + imageFolder, imageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
    }
}

