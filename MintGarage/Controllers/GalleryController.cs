using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MintGarage.Models;
using MintGarage.Models.FooterContents.FooterContactInfo;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using MintGarage.Models.GalleryTab;
using MintGarage.Models.Partners;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Controllers
{
    public class GalleryController : Controller
    {
        private IGalleryRepository galleryRepo;
        public IPartnerRepository partnerRepo;
        private IFooterContactInfoRepository footerContactInfoRepo;
        private IFooterSocialMediaRepository footerSocialMediaRepo;
        private IWebHostEnvironment hostEnv;
        GalleryModel galleryModel = new GalleryModel();

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
            "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";
        private string imageFolder = "/Images/";

    

        public GalleryController(IGalleryRepository galleryRepository, IPartnerRepository partnerRepository,
                                                IFooterContactInfoRepository contactInfoRepository, IFooterSocialMediaRepository socialMediaRepository,
                                                IWebHostEnvironment hostEnvironment)
        {
            galleryRepo = galleryRepository;
            partnerRepo = partnerRepository;
            footerContactInfoRepo = contactInfoRepository;
            footerSocialMediaRepo = socialMediaRepository;
            hostEnv = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

            
        public IActionResult Update(int? id, string? operation, bool? show)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.message = TempData["message"];
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            SetViewBag(false, false, false);
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

            galleryModel.Galleries = galleryRepo.Galleries;

            if (id != null && operation != "add")
            {
                galleryModel.Gallery = galleryRepo.Galleries.FirstOrDefault(s => s.GalleryID == id);
            }
            return View(galleryModel);
        }

        
        public async Task<IActionResult> Create(GalleryModel galleryModel)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid && galleryModel.Gallery.BeforeImageFile != null && galleryModel.Gallery.AfterImageFile != null)
            {
                galleryModel.Gallery.BeforeImage = await SaveImage(galleryModel.Gallery.BeforeImageFile);
                galleryModel.Gallery.AfterImage = await SaveImage(galleryModel.Gallery.AfterImageFile);

                galleryRepo.Add(galleryModel.Gallery);
                TempData["message"] = "Successfully added new Gallery Images.";
            }
            else
            {
                if (galleryModel.Gallery.BeforeImageFile == null || galleryModel.Gallery.AfterImageFile == null)
                {
                    ModelState.AddModelError("Image", "Before and After images are required");
                }
                galleryModel.Galleries = galleryRepo.Galleries;
                SetViewBag(true, false, false);
                return View("Update", galleryModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> Edit(GalleryModel galleryModel)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                if (galleryModel.Gallery.BeforeImageFile != null)
                {
                    DeleteImage(galleryModel.Gallery.BeforeImage);
                    galleryModel.Gallery.BeforeImage = await SaveImage(galleryModel.Gallery.BeforeImageFile);
                }
                if(galleryModel.Gallery.AfterImageFile != null)
                {
                    DeleteImage(galleryModel.Gallery.AfterImage);
                    galleryModel.Gallery.AfterImage = await SaveImage(galleryModel.Gallery.AfterImageFile);
                }
                galleryRepo.Update(galleryModel.Gallery);
                TempData["message"] = "Successfully edited Gallery.";
            }
            else
            {
                galleryModel.Galleries = galleryRepo.Galleries;
                SetViewBag(false, true, false);
                return View("Update", galleryModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult Delete(GalleryModel galleryModel)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            DeleteImage(galleryModel.Gallery.BeforeImage);
            DeleteImage(galleryModel.Gallery.AfterImage);
            galleryRepo.Delete(galleryModel.Gallery);
            TempData["message"] = "Successfully deleted Gallery.";
            return RedirectToAction("Update");
        }


        public void SetViewBag(bool add, bool edit, bool delete)
        {
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
