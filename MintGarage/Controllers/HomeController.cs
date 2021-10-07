using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MintGarage.Models;
using MintGarage.Models.Partners;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MintGarage.Models.HomeTab.HomeContents;
using MintGarage.Models.HomeTab.Reviews;
using MintGarage.Models.HomeTab.Suppliers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using MintGarage.Models.FooterContents.FooterContactInfo;

namespace MintGarage.Controllers
{
    public class HomeController : Controller
    {
        public IPartnerRepository partnerRepository;
        private IHomeContentRepository homeContentRepo;
        private IReviewRepository reviewRepo;
        private ISupplierRepository supplierRepo;
        private IFooterContactInfoRepository footerContactInfoRepo;
        private IFooterSocialMediaRepository footerSocialMediaRepo;

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
            "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";
        private IWebHostEnvironment hostEnv;
        private string imageFolder = "/Images/";

        public HomeController(IPartnerRepository partnerRepo, IHomeContentRepository homeContentRepository, 
                                            IReviewRepository reviewRepository, ISupplierRepository supplierRepository,
                                            IWebHostEnvironment hostEnvironment, IFooterSocialMediaRepository socialMediaRepository,
                                            IFooterContactInfoRepository contactRepository)
        {
            partnerRepository = partnerRepo;
            homeContentRepo = homeContentRepository;
            reviewRepo = reviewRepository;
            supplierRepo = supplierRepository;
            footerSocialMediaRepo = socialMediaRepository;
            footerContactInfoRepo = contactRepository;
            hostEnv = hostEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            HomeModel homeModel = new HomeModel()
            {
                HomeContents = homeContentRepo.HomeContents,
                Reviews = reviewRepo.Reviews,
                Suppliers = supplierRepo.Suppliers,
            };
            return View(homeModel);
        }

        public IActionResult Update(int? id, string? operation, bool? show, string? table)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.message = TempData["message"];
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            setViewBag(false, false, false, "");
            if (operation != null && show != null && table != "")
            {
                ViewBag.table = table;
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


            HomeModel homeModel = new HomeModel();
            homeModel.HomeContents = homeContentRepo.HomeContents;
            homeModel.Reviews = reviewRepo.Reviews;
            homeModel.Suppliers = supplierRepo.Suppliers;

            if (id != null && operation != "add")
            {
                homeModel.HomeContent = homeContentRepo.HomeContents.FirstOrDefault(s => s.HomeContentsID == id);
                homeModel.Review = reviewRepo.Reviews.FirstOrDefault(s => s.ReviewsID == id);
                homeModel.Supplier = supplierRepo.Suppliers.FirstOrDefault(s => s.SuppliersID == id);
            }
            return View(homeModel);
        }

        public async Task<IActionResult> CreateHomeContent(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                homeModel.HomeContent.Image = await SaveImage(homeModel.HomeContent.ImageFile);
                homeContentRepo.AddHomeContents(homeModel.HomeContent);
                TempData["message"] = "Successfully added new Home Content.";
            }
            else
            {
                homeModel.HomeContents = homeContentRepo.HomeContents;
                homeModel.Reviews = reviewRepo.Reviews;
                homeModel.Suppliers = supplierRepo.Suppliers;
                setViewBag(true, false, false, "homecontent");

                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> EditHomeContent(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                DeleteImage(homeModel.HomeContent.Image);
                homeModel.HomeContent.Image = await SaveImage(homeModel.HomeContent.ImageFile);
                homeContentRepo.UpdateHomeContents(homeModel.HomeContent);
                TempData["message"] = "Successfully edited Home Content.";
            }
            if(!ModelState.IsValid)
            {
                homeModel.HomeContents = homeContentRepo.HomeContents;
                homeModel.Reviews = reviewRepo.Reviews;
                homeModel.Suppliers = supplierRepo.Suppliers;

                setViewBag(false, true, false, "homecontent");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult DeleteHomeContent(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            DeleteImage(homeModel.HomeContent.Image);
            homeContentRepo.DeleteHomeContents(homeModel.HomeContent);
            TempData["message"] = "Successfully deleted Home Content.";
            return RedirectToAction("Update");
        }

        public IActionResult CreateReview(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                reviewRepo.AddReviews(homeModel.Review);
                TempData["message"] = "Successfully added new Review.";
            }
            else
            {
                homeModel.HomeContents = homeContentRepo.HomeContents;
                homeModel.Reviews = reviewRepo.Reviews;
                homeModel.Suppliers = supplierRepo.Suppliers;

                setViewBag(true, false, false, "review");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult EditReview(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                reviewRepo.UpdateReviews(homeModel.Review);
                TempData["message"] = "Successfully edited Review.";
            }
            else
            {
                homeModel.HomeContents = homeContentRepo.HomeContents;
                homeModel.Reviews = reviewRepo.Reviews;
                homeModel.Suppliers = supplierRepo.Suppliers;

                setViewBag(false, true, false, "review");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult DeleteReview(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            reviewRepo.DeleteReviews(homeModel.Review);
            TempData["message"] = "Successfully deleted Review.";
            return RedirectToAction("Update");
        }


        public async Task<IActionResult> CreateSupplier(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                homeModel.Supplier.SupplierLogo = await SaveImage(homeModel.Supplier.ImageFile);
                supplierRepo.AddSuppliers(homeModel.Supplier);
                TempData["message"] = "Successfully added new Supplier.";
            }
            else
            {
                homeModel.HomeContents = homeContentRepo.HomeContents;
                homeModel.Reviews = reviewRepo.Reviews;
                homeModel.Suppliers = supplierRepo.Suppliers;
                setViewBag(true, false, false, "supplier");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> EditSupplier(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                DeleteImage(homeModel.Supplier.SupplierLogo);
                homeModel.HomeContent.Image = await SaveImage(homeModel.Supplier.ImageFile);
                supplierRepo.UpdateSuppliers(homeModel.Supplier);
                TempData["message"] = "Successfully edited Supplier.";
            }
            else
            {
                homeModel.HomeContents = homeContentRepo.HomeContents;
                homeModel.Reviews = reviewRepo.Reviews;
                homeModel.Suppliers = supplierRepo.Suppliers;
                setViewBag(false, true, false, "supplier");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult DeleteSupplier(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            DeleteImage(homeModel.Supplier.SupplierLogo);
            supplierRepo.DeleteSuppliers(homeModel.Supplier);
            TempData["message"] = "Successfully deleted Supplier.";
            return RedirectToAction("Update");
        }


        public void setViewBag(bool add, bool edit, bool delete, string table)
        {
            ViewBag.add = add;
            ViewBag.edit = edit;
            ViewBag.delete = delete;
            ViewBag.table = table;
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
            if (System.IO.File.Exists(imagePath)){
                System.IO.File.Delete(imagePath);
            }
        }

        public IActionResult Privacy()
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
            
    }

}





