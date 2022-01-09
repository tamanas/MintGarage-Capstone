using Microsoft.AspNetCore.Mvc;
using MintGarage.Models.AccountT;
using System;
using System.Linq;
using MintGarage.Models.PartnerT;
using MintGarage.Models.FooterT.SocialMedias;
using MintGarage.Models.FooterT.ContactInformation;
using MintGarage.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using BC = BCrypt.Net.BCrypt;

namespace MintGarage.Controllers
{
    public class AccountController : Controller
    {
        public IRepository<Account> accoutRepo;
        public IRepository<Partner> partnerRepo;
        private IRepository<ContactInfo> contactInfoRepo;
        private IRepository<SocialMedia> socialMediaRepo;

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
        "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";

        public AccountController(IRepository<Account> accountRepository, IRepository<Partner> partnerRepository, 
            IRepository<ContactInfo> contactRepo, IRepository<SocialMedia> mediaRepo)
        {
            contactInfoRepo = contactRepo;
            socialMediaRepo = mediaRepo;
            accoutRepo = accountRepository;
            partnerRepo = partnerRepository;
        }

        public IActionResult Login()
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            ViewBag.Message = TempData["Message"];
            ViewBag.Success = TempData["Success"];
            HttpContext.Session.SetString("isAdminLoggedIn", "false");

            Account acc = accoutRepo.Items.FirstOrDefault();
            // Hash password
            if (acc.Password.Length <= 32)
            {
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(acc.Password);
                acc.Password = hashPassword;
                accoutRepo.Update(acc);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Account account)
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                Account acc = accoutRepo.Items.FirstOrDefault();

                bool verifyPassword = BCrypt.Net.BCrypt.Verify(account.Password, acc.Password);

                if (acc.Username == account.Username && verifyPassword)
                {
                    HttpContext.Session.SetString("isAdminLoggedIn", "true");
                    return RedirectToAction("Update", "Home");
                }
                ModelState.AddModelError("error", "Invalid username or password. Please try again!");
            }
            return View();
        }

        public IActionResult Update()
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdatePassword updatePassword)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("true"))
            {
                if (ModelState.IsValid)
                {
                    Account acc = accoutRepo.Items.FirstOrDefault();

                    bool verifyPassword = BCrypt.Net.BCrypt.Verify(updatePassword.CurrectPassword, acc.Password);

                    if (!verifyPassword)
                    {
                        TempData["Message"] = "Incorrect current password. Please try again!";
                        TempData["Success"] = false;
                    }
                    if (verifyPassword)
                    {
                        string hashPassword = BCrypt.Net.BCrypt.HashPassword(updatePassword.NewPassword);
                        acc.Password = hashPassword;
                        accoutRepo.Update(acc);
                        TempData["Message"] = "Password updated successfully.";
                        TempData["Success"] = true;
                    }
                    return RedirectToAction("Update");
                }
            } else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Logout()
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            HttpContext.Session.SetString("isAdminLoggedIn", "false");
            return RedirectToAction("Index", "Home");
        }
    }
}
