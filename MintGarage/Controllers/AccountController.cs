using Microsoft.AspNetCore.Mvc;
using MintGarage.Models.AccountT;
using System;
using System.Linq;
using MintGarage.Models.PartnerT;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using MintGarage.Models.FooterContents.FooterContactInfo;
using MintGarage.Models;

namespace MintGarage.Controllers
{
    public class AccountController : Controller
    {
        public IRepository<Account> accoutRepo;
        public IRepository<Partner> partnerRepo;
        private IFooterContactInfoRepository footerContactInfoRepository;
        private IFooterSocialMediaRepository footerSocialMediaRepository;

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
        "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";

        public AccountController(IRepository<Account> accountRepository, IRepository<Partner> partnerRepository, 
            IFooterContactInfoRepository footerContactInfoRepo, IFooterSocialMediaRepository footerSocialMediaRepo)
        {
            footerContactInfoRepository = footerContactInfoRepo;
            footerSocialMediaRepository = footerSocialMediaRepo;
            accoutRepo = accountRepository;
            partnerRepo = partnerRepository;
        }

        public IActionResult Login()
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            ViewBag.Message = TempData["Message"];
            ViewBag.Success = TempData["Success"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Account account)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            if (ModelState.IsValid)
            {
                Account acc = accoutRepo.Items.FirstOrDefault();
                if (acc.Username == account.Username && acc.Password == account.Password)
                {
                    return RedirectToAction("Update", "Home");
                }
                ModelState.AddModelError("error", "Invalid username or password. Please try again!");
            }
            return View();
        }

        public IActionResult Update()
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            ViewBag.Message = TempData["Message"];
            ViewBag.Success = TempData["Success"];
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdatePassword updatePassword)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            if (ModelState.IsValid)
            {
                Account acc = accoutRepo.Items.FirstOrDefault();
                
                if (!acc.Password.Equals(updatePassword.CurrectPassword))
                {
                    TempData["Message"] = "Incorrect current password. Please try again!";
                    TempData["Success"] = false;
                }
                if (acc.Password.Equals(updatePassword.CurrectPassword))
                {
                    acc.Password = updatePassword.NewPassword;
                    accoutRepo.Update(acc);
                    TempData["Message"] = "Password updated successfully.";
                    TempData["Success"] = true;
                }
                return RedirectToAction("Update");
            }
            return View();
        }

        public IActionResult Logout()
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            return RedirectToAction("index", "Home");
        }
    }
}
