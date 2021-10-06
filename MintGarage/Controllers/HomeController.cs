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

namespace MintGarage.Controllers
{
    public class HomeController : Controller
    {
        private IHomeContentRepository homeContentRepo;
        private IReviewRepository reviewRepo;
        private ISupplierRepository supplierRepo;
        public IPartnerRepository partnerRepository;

        public HomeController(IHomeContentRepository homeContentRepository, IReviewRepository reviewRepository, 
            ISupplierRepository suuplierRepository, IPartnerRepository partnerRepo)
        {
            homeContentRepo = homeContentRepository;
            reviewRepo = reviewRepository;
            supplierRepo = suuplierRepository;
            partnerRepository = partnerRepo;
        }

        public IActionResult Index()
        {
            ViewBag.Partners = partnerRepository.Partners;
            var homeContentsList = homeContentRepo.HomeContents;
            var reviewList = reviewRepo.Reviews;
            var suplierList = supplierRepo.Suppliers;

            HomeModel homeModel = new HomeModel()
            {
                HomeContent = homeContentsList,
                Review = reviewList,
                Supplier = suplierList,
            };
            return View(homeModel);
        }

        public IActionResult Update()
        {
            ViewBag.Partners = partnerRepository.Partners;
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.Partners = partnerRepository.Partners;
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
            ViewBag.Partners = partnerRepository.Partners;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
            
    }

}





