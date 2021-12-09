using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MintGarage.Models;
using MintGarage.Models.FooterT.ContactInformation;
using MintGarage.Models.FooterT.SocialMedias;
using MintGarage.Models.GalleryT;
using MintGarage.Models.PartnerT;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Controllers
{
    public class GalleryController : Controller
    {
        private IRepository<Gallery> galleryRepo;
        public IRepository<Partner> partnerRepo;
        private IRepository<ContactInfo> contactInfoRepo;
        private IRepository<SocialMedia> socialMediaRepo;
        private IWebHostEnvironment hostEnv;
        GalleryModel galleryModel = new GalleryModel();

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
            "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";
        private string imageFolder = "/Images/gallery/";



        public GalleryController(IRepository<Gallery> galleryRepository, IRepository<Partner> partnerRepository,
                                                IRepository<ContactInfo> contactInfoRepository, IRepository<SocialMedia> socialMediaRepository,
                                                IWebHostEnvironment hostEnvironment)
        {
            galleryRepo = galleryRepository;
            partnerRepo = partnerRepository;
            contactInfoRepo = contactInfoRepository;
            socialMediaRepo = socialMediaRepository;
            hostEnv = hostEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            galleryModel.Galleries = galleryRepo.Items;
            HttpContext.Session.SetString("isAdminLoggedIn", "false");
            return View(galleryModel);
        }

        public IActionResult Update(int? id, string? operation, bool? show)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Partners = partnerRepo.Items;
            ViewBag.message = TempData["message"];
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
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

            galleryModel.Galleries = galleryRepo.Items;

            if (id != null && operation != "add")
            {
                galleryModel.Gallery = galleryRepo.Items.FirstOrDefault(s => s.GalleryID == id);
            }
            return View(galleryModel);
        }


        public async Task<IActionResult> Create(GalleryModel galleryModel)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            if (galleryModel.Gallery.BeforeImageFile != null && galleryModel.Gallery.AfterImageFile != null)
            {
                galleryModel.Gallery.BeforeImage = await SaveImage(galleryModel.Gallery.BeforeImageFile);
                galleryModel.Gallery.AfterImage = await SaveImage(galleryModel.Gallery.AfterImageFile);

                galleryRepo.Create(galleryModel.Gallery);
                TempData["message"] = "Successfully added new Gallery Images.";
            }
            else
            {
                if (galleryModel.Gallery.BeforeImageFile == null || galleryModel.Gallery.AfterImageFile == null)
                {
                    ModelState.AddModelError("Image", "Before and After images are required");
                }
                galleryModel.Galleries = galleryRepo.Items;
                SetViewBag(true, false, false);
                return View("Update", galleryModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> Edit(GalleryModel galleryModel)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                if (galleryModel.Gallery.BeforeImageFile != null)
                {
                    DeleteImage(galleryModel.Gallery.BeforeImage);
                    galleryModel.Gallery.BeforeImage = await SaveImage(galleryModel.Gallery.BeforeImageFile);
                }
                if (galleryModel.Gallery.AfterImageFile != null)
                {
                    DeleteImage(galleryModel.Gallery.AfterImage);
                    galleryModel.Gallery.AfterImage = await SaveImage(galleryModel.Gallery.AfterImageFile);
                }
                galleryRepo.Update(galleryModel.Gallery);
                TempData["message"] = "Successfully edited Gallery.";
            }
            else
            {
                galleryModel.Galleries = galleryRepo.Items;
                SetViewBag(false, true, false);
                return View("Update", galleryModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult Delete(GalleryModel galleryModel)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
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