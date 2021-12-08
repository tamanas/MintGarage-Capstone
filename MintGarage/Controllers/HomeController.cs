using Microsoft.AspNetCore.Mvc;
using MintGarage.Models;
using MintGarage.Models.PartnerT;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MintGarage.Models.HomeT.Cards;
using MintGarage.Models.HomeT.Reviews;
using MintGarage.Models.HomeT.Suppliers;
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
        private IRepository<Card> cardRepo;
        private IRepository<Review> reviewRepo;
        private IRepository<Supplier> supplierRepo;
        private IRepository<ContactInfo> contactInfoRepo;
        private IRepository<SocialMedia> socialMediaRepo;

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
            "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";
        private IWebHostEnvironment hostEnv;
        private string imageFolder = "/Images/home/";

        public HomeController(IRepository<Partner> partnerRepository, IRepository<Card> cardRepository, 
                                            IRepository<Review> reviewRepository, IRepository<Supplier> supplierRepository,
                                            IWebHostEnvironment hostEnvironment, IRepository<SocialMedia> mediaRepository,
                                            IRepository<ContactInfo> contactRepository)
        {
            partnerRepo = partnerRepository;
            cardRepo = cardRepository;
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
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            //HttpContext.Session.SetString("isAdminLoggedIn", "false");
            HomeModel homeModel = new HomeModel()
            {
                Cards = cardRepo.Items,
                Reviews = reviewRepo.Items,
                Suppliers = supplierRepo.Items,
            };
            return View(homeModel);
        }

        public IActionResult Update(int? id, string? operation, bool? show, string? table)
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
            homeModel.Cards = cardRepo.Items;
            homeModel.Reviews = reviewRepo.Items;
            homeModel.Suppliers = supplierRepo.Items;

            if (id != null && operation != "add")
            {
                homeModel.Card = cardRepo.Items.FirstOrDefault(s => s.CardID == id);
                homeModel.Review = reviewRepo.Items.FirstOrDefault(s => s.ReviewsID == id);
                homeModel.Supplier = supplierRepo.Items.FirstOrDefault(s => s.SuppliersID == id);
            }
            return View(homeModel);
        }

        public async Task<IActionResult> CreateCard(HomeModel homeModel)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid && homeModel.Card.ImageFile != null)
            {
                homeModel.Card.Image = await SaveImage(homeModel.Card.ImageFile);
                cardRepo.Create(homeModel.Card);
                TempData["message"] = "Successfully added new Card.";
            }
            else
            {
                if(homeModel.Card.ImageFile == null)
                {
                    ModelState.AddModelError("image", "Image is required");
                }
                homeModel.Cards = cardRepo.Items;
                homeModel.Reviews = reviewRepo.Items;
                homeModel.Suppliers = supplierRepo.Items;
                SetViewBag(true, false, false, "card");

                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> EditCard(HomeModel homeModel)
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
                if(homeModel.Card.ImageFile != null)
                {
                    DeleteImage(homeModel.Card.Image);
                    homeModel.Card.Image = await SaveImage(homeModel.Card.ImageFile);
                }
                cardRepo.Update(homeModel.Card);
                TempData["message"] = "Successfully edited Card.";
            }
            if(!ModelState.IsValid)
            {
                homeModel.Cards = cardRepo.Items;
                homeModel.Reviews = reviewRepo.Items;
                homeModel.Suppliers = supplierRepo.Items;

                SetViewBag(false, true, false, "card");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult DeleteCard(HomeModel homeModel)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            DeleteImage(homeModel.Card.Image);
            cardRepo.Delete(homeModel.Card);
            TempData["message"] = "Successfully deleted Card.";
            return RedirectToAction("Update");
        }

        public IActionResult CreateReview(HomeModel homeModel)
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
                reviewRepo.Create(homeModel.Review);
                TempData["message"] = "Successfully added new Review.";
            }
            else
            {
                homeModel.Cards = cardRepo.Items;
                homeModel.Reviews = reviewRepo.Items;
                homeModel.Suppliers = supplierRepo.Items;

                SetViewBag(true, false, false, "review");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult EditReview(HomeModel homeModel)
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
                reviewRepo.Update(homeModel.Review);
                TempData["message"] = "Successfully edited Review.";
            }
            else
            {
                homeModel.Cards = cardRepo.Items;
                homeModel.Reviews = reviewRepo.Items;
                homeModel.Suppliers = supplierRepo.Items;

                SetViewBag(false, true, false, "review");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult DeleteReview(HomeModel homeModel)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            reviewRepo.Delete(homeModel.Review);
            TempData["message"] = "Successfully deleted Review.";
            return RedirectToAction("Update");
        }


        public async Task<IActionResult> CreateSupplier(HomeModel homeModel)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            
            if (ModelState.IsValid && homeModel.Supplier.ImageFile != null)
            {
                homeModel.Supplier.SupplierLogo = await SaveImage(homeModel.Supplier.ImageFile);
                supplierRepo.Create(homeModel.Supplier);
                TempData["message"] = "Successfully added new Supplier.";
            }
            else
            {
                if (homeModel.Supplier.ImageFile == null)
                {
                    ModelState.AddModelError("Image", "Image is required");
                }
                homeModel.Cards = cardRepo.Items;
                homeModel.Reviews = reviewRepo.Items;
                homeModel.Suppliers = supplierRepo.Items;
                SetViewBag(true, false, false, "supplier");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> EditSupplier(HomeModel homeModel)
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
                if (homeModel.Supplier.ImageFile != null)
                {
                    DeleteImage(homeModel.Supplier.SupplierLogo);
                    homeModel.Supplier.SupplierLogo = await SaveImage(homeModel.Supplier.ImageFile);
                }
                supplierRepo.Update(homeModel.Supplier);
                TempData["message"] = "Successfully edited Supplier.";
            }
            else
            {
                homeModel.Cards = cardRepo.Items;
                homeModel.Reviews = reviewRepo.Items;
                homeModel.Suppliers = supplierRepo.Items;
                SetViewBag(false, true, false, "supplier");
                return View("Update", homeModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult DeleteSupplier(HomeModel homeModel)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            DeleteImage(homeModel.Supplier.SupplierLogo);
            supplierRepo.Delete(homeModel.Supplier);
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