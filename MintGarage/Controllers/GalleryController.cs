using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MintGarage.Database;
using MintGarage.Models;
using MintGarage.Models.GalleryTab;
using MintGarage.Models.Partners;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using MintGarage.Models.FooterContents.FooterContactInfo;

namespace MintGarage.Controllers
{
    public class GalleryController : Controller
    {
        private IGalleryRepository galleryRepo;
        public IPartnerRepository partnerRepo;
        private IFooterContactInfoRepository footerContactInfoRepo;
        private IFooterSocialMediaRepository footerSocialMediaRepo;

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
            "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";
        private IWebHostEnvironment hostEnv;
        private string imageFolder = "/Images/";

        private readonly MintGarageContext _context;

        public GalleryController(IGalleryRepository galleryRepository, IPartnerRepository partnerRepository, IFooterContactInfoRepository footerContactInfoRepository, IFooterSocialMediaRepository footerSocialMediaRepository, IWebHostEnvironment hostEnvironment, MintGarageContext context)
        {
            galleryRepo = galleryRepository;
            partnerRepo = partnerRepository;
            footerContactInfoRepo = footerContactInfoRepository;
            footerSocialMediaRepo = footerSocialMediaRepository;
            hostEnv = hostEnvironment;
            _context = context;
        }

        // GET: Galleries
        public async Task<IActionResult> Index()
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            return View(await _context.Gallery.ToListAsync());
        }
    }
}
