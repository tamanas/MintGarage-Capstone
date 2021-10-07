using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MintGarage.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MintGarage.Models.HomeTab.HomeContents;
using MintGarage.Models.HomeTab.Reviews;
using MintGarage.Models.HomeTab.Suppliers;
using MintGarage.Models.HomeTab.SocialMedias;
using MintGarage.Models.HomeTab.Contacts;

namespace MintGarage.Controllers
{
    public class HomeController : Controller
    {
        private IHomeContentRepository homeContentRepo;
        private IReviewRepository reviewRepo;
        private ISupplierRepository supplierRepo;
        private ISocialMediaRepository socialMediaRepo;
        private IContactRepository contactRepo;

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
            "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";

        public HomeController(IHomeContentRepository homeContentRepository, IReviewRepository reviewRepository, ISupplierRepository supplierRepository, ISocialMediaRepository socialMediaRepository, IContactRepository contactRepository)
        {
            homeContentRepo = homeContentRepository;
            reviewRepo = reviewRepository;
            supplierRepo = supplierRepository;
            socialMediaRepo = socialMediaRepository;
            contactRepo = contactRepository;
        }

        public IActionResult Index()
        {
            ViewBag.SocialMedias = socialMediaRepo.SocialMedias;
            ViewBag.Contacts = contactRepo.Contacts;
            ViewBag.AboutData = AboutUs;

            HomeModel homeModel = new HomeModel()
            {
                HomeContent = homeContentRepo.HomeContents,
                Review = reviewRepo.Reviews,
                Supplier = supplierRepo.Suppliers,
            };

            return View(homeModel);
        }

        public IActionResult Update()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UpdateContactUs()
        {
            return View();
        }

        public IActionResult UpdateSocial()
        {
            return View();
        }

        public IActionResult UpdateReviews()
        {
            return View();
        }

        public IActionResult UpdateSuppliers()
        {
            return View();
        }

        public IActionResult UpdateHome()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
            
    }

}





