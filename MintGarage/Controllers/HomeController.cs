using Microsoft.AspNetCore.Mvc;
using MintGarage.Models;
using MintGarage.Models.PartnerT;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MintGarage.Models.HomeTab.HomeContents;
using MintGarage.Models.HomeTab.Reviews;
using MintGarage.Models.HomeTab.Suppliers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using MintGarage.Models.FooterT.SocialMedias;
using MintGarage.Models.FooterT.ContactInformation;

namespace MintGarage.Controllers
{
    public class HomeController : Controller
    {
        public IRepository<Partner> partnerRepo;
        private IHomeContentRepository homeContentRepo;
        private IReviewRepository reviewRepo;
        private ISupplierRepository supplierRepo;
        private IRepository<ContactInfo> contactInfoRepo;
        private IRepository<SocialMedia> socialMediaRepo;

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
            "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";
        private IWebHostEnvironment hostEnv;
        private string imageFolder = "/Images/";

        public HomeController(IRepository<Partner> partnerRepository, IHomeContentRepository homeContentRepository, 
                                            IReviewRepository reviewRepository, ISupplierRepository supplierRepository,
                                            IWebHostEnvironment hostEnvironment, IRepository<SocialMedia> mediaRepository,
                                            IRepository<ContactInfo> contactRepository)
        {
            partnerRepo = partnerRepository;
            homeContentRepo = homeContentRepository;
            reviewRepo = reviewRepository;
            supplierRepo = supplierRepository;
            socialMediaRepo = mediaRepository;
            contactInfoRepo = contactRepository;
            hostEnv = hostEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
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
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.message = TempData["message"];
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            SetViewBag(false, false, false, "");
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
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid && homeModel.HomeContent.ImageFile != null)
            {
                homeModel.HomeContent.Image = await SaveImage(homeModel.HomeContent.ImageFile);
                homeContentRepo.AddHomeContents(homeModel.HomeContent);
                TempData["message"] = "Successfully added new Home Content.";
            }
            else
            {
                if(homeModel.HomeContent.ImageFile == null)
                {
                    ModelState.AddModelError("image", "Image is required");
                }
                homeModel.HomeContents = homeContentRepo.HomeContents;
                homeModel.Reviews = reviewRepo.Reviews;
                homeModel.Suppliers = supplierRepo.Suppliers;
                SetViewBag(true, false, false, "homecontent");

                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> EditHomeContent(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                if(homeModel.HomeContent.ImageFile != null)
                {
                    DeleteImage(homeModel.HomeContent.Image);
                    homeModel.HomeContent.Image = await SaveImage(homeModel.HomeContent.ImageFile);
                }
                homeContentRepo.UpdateHomeContents(homeModel.HomeContent);
                TempData["message"] = "Successfully edited Home Content.";
            }
            if(!ModelState.IsValid)
            {
                homeModel.HomeContents = homeContentRepo.HomeContents;
                homeModel.Reviews = reviewRepo.Reviews;
                homeModel.Suppliers = supplierRepo.Suppliers;

                SetViewBag(false, true, false, "homecontent");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult DeleteHomeContent(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            DeleteImage(homeModel.HomeContent.Image);
            homeContentRepo.DeleteHomeContents(homeModel.HomeContent);
            TempData["message"] = "Successfully deleted Home Content.";
            return RedirectToAction("Update");
        }

        public IActionResult CreateReview(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
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

                SetViewBag(true, false, false, "review");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult EditReview(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
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

                SetViewBag(false, true, false, "review");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult DeleteReview(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            reviewRepo.DeleteReviews(homeModel.Review);
            TempData["message"] = "Successfully deleted Review.";
            return RedirectToAction("Update");
        }


        public async Task<IActionResult> CreateSupplier(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            
            if (ModelState.IsValid && homeModel.Supplier.ImageFile != null)
            {
                homeModel.Supplier.SupplierLogo = await SaveImage(homeModel.Supplier.ImageFile);
                supplierRepo.AddSuppliers(homeModel.Supplier);
                TempData["message"] = "Successfully added new Supplier.";
            }
            else
            {
                if (homeModel.Supplier.ImageFile == null)
                {
                    ModelState.AddModelError("Image", "Image is required");
                }
                homeModel.HomeContents = homeContentRepo.HomeContents;
                homeModel.Reviews = reviewRepo.Reviews;
                homeModel.Suppliers = supplierRepo.Suppliers;
                SetViewBag(true, false, false, "supplier");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> EditSupplier(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                if (homeModel.Supplier.ImageFile != null)
                {
                    DeleteImage(homeModel.Supplier.SupplierLogo);
                    homeModel.HomeContent.Image = await SaveImage(homeModel.Supplier.ImageFile);
                }
                supplierRepo.UpdateSuppliers(homeModel.Supplier);
                TempData["message"] = "Successfully edited Supplier.";
            }
            else
            {
                homeModel.HomeContents = homeContentRepo.HomeContents;
                homeModel.Reviews = reviewRepo.Reviews;
                homeModel.Suppliers = supplierRepo.Suppliers;
                SetViewBag(false, true, false, "supplier");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult DeleteSupplier(HomeModel homeModel)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            DeleteImage(homeModel.Supplier.SupplierLogo);
            supplierRepo.DeleteSuppliers(homeModel.Supplier);
            TempData["message"] = "Successfully deleted Supplier.";
            return RedirectToAction("Update");
        }


        public void SetViewBag(bool add, bool edit, bool delete, string table)
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
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
            
    }
}