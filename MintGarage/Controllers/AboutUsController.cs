using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MintGarage.Models;
using MintGarage.Models.AboutUsTab.Teams;
using MintGarage.Models.AboutUsTab.Values;
using MintGarage.Models.FooterContents.FooterContactInfo;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using MintGarage.Models.HomeTab.HomeContents;
using MintGarage.Models.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MintGarage.Controllers
{
    public class AboutUsController : Controller
    {
        public IPartnerRepository partnerRepository;
        private IFooterContactInfoRepository footerContactInfoRepo;
        private IFooterSocialMediaRepository footerSocialMediaRepo;
        private ITeamRepository teamRepo;
        private IValueRepository valueRepo;

        private IWebHostEnvironment hostEnv;
        private string imageFolder = "/Images/";

        public AboutUsController(ITeamRepository teamRepository, IValueRepository valueRepository, IFooterSocialMediaRepository socialMediaRepository,
                                            IFooterContactInfoRepository contactRepository, IPartnerRepository partnerRepo, IWebHostEnvironment hostEnvironment)
        {
            partnerRepository = partnerRepo;
            teamRepo = teamRepository;
            valueRepo = valueRepository;
            footerSocialMediaRepo = socialMediaRepository;
            footerContactInfoRepo = contactRepository;
            hostEnv = hostEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            AboutUsModel aboutUs = new AboutUsModel()
            
            {
                Teams = teamRepo.Teams,
                Values = valueRepo.Values,
            };
            return View(aboutUs);
        }


        public IActionResult Update()
        {
            return View();
        }
    }
}
