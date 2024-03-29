﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MintGarage.Models.ConsultationT;
using MintGarage.Models.PartnerT;
using MintGarage.Models.FooterT.SocialMedias;
using MintGarage.Models.FooterT.ContactInformation;
using MintGarage.Models;
using Microsoft.AspNetCore.Http;

namespace MintGarage.Controllers
{
    public class ConsultationController : Controller
    {
        public IRepository<Consultation> consultationRepo;
        public IRepository<Partner> partnerRepo;
        private IRepository<ContactInfo> contactInfoRepo;
        private IRepository<SocialMedia> socialMediaRepo;
        public ConsultationModel consultationModel = new ConsultationModel();

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
        "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";

        public ConsultationController(IRepository<Consultation> consultationRepository, IRepository<Partner> partnerRepository,
            IRepository<ContactInfo> contactRepo, IRepository<SocialMedia> mediaRepo)
        {
            consultationRepo = consultationRepository;
            partnerRepo = partnerRepository;
            contactInfoRepo = contactRepo;
            socialMediaRepo = mediaRepo;
        }

        public IActionResult Update(string sortCol, bool sort, string searchString)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }

            var forms = consultationRepo.Items;
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            ViewData["CurrentFilter"] = searchString;

            // Search Function
            if (!String.IsNullOrEmpty(searchString))
            {
                forms = forms.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString)
                                       || s.LastName.Contains(searchString)
                                       || s.EmailAddress.Contains(searchString)
                                       || s.PhoneNumber.Contains(searchString)
                                       || s.ServiceType.Contains(searchString)
                                       || s.FormDescription.Contains(searchString) );
            }

            ViewData["allow"] = !sort;

            // Sort Function
            switch (sortCol)
            {
                case "fname":
                    if (sort) forms = forms.OrderByDescending(s => s.FirstName);
                    else forms = forms.OrderBy(s => s.FirstName);
                    break;
                case "lname":
                    if (sort) forms = forms.OrderByDescending(s => s.LastName);
                    else forms = forms.OrderBy(s => s.LastName);
                    break;
                case "email":
                    if (sort) forms = forms.OrderByDescending(s => s.EmailAddress);
                    else forms = forms.OrderBy(s => s.EmailAddress);
                    break;
                case "service":
                    if (sort) forms = forms.OrderByDescending(s => s.ServiceType);
                    else forms = forms.OrderBy(s => s.ServiceType);
                    break;
                case "description":
                    if (sort) forms = forms.OrderByDescending(s => s.FormDescription);
                    else forms = forms.OrderBy(s => s.FormDescription);
                    break;
                default:
                    forms = forms.OrderBy(s => s.FirstName);
                    break;
            }
            consultationModel.Consultations = forms;
            return View(consultationModel);
        }


        // GET: ConsultationForms/Index
        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Success = TempData["Success"];
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            HttpContext.Session.SetString("isAdminLoggedIn", "false");
            return View(consultationModel);
        }

        // POST: ConsultationForms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ConsultationModel consultationModel)
        {
            HttpContext.Session.SetString("isAdminLoggedIn", "false");

            if (ModelState.IsValid)
            {
                new Email(consultationModel.Consultation).SendEmail();
                try
                {
                    consultationRepo.Create(consultationModel.Consultation);
                    TempData["Message"] = "Your consultation request has been sent successfully.";
                    TempData["Success"] = true;
                }
                catch (Exception e)
                {
                    TempData["Message"] = "Unable to send consultation request.";
                    TempData["Success"] = false;
                }
                return RedirectToAction("Index");
            } else
            {
                TempData["Message"] = "";
                TempData["Success"] = false;
            }
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            return View(consultationModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }
            var consultation = await consultationRepo.Items
                .FirstOrDefaultAsync(m => m.ConsultationID == id);
            if (consultation == null)
            {
                return NotFound();
            }
            consultationRepo.Delete(consultation);
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            return RedirectToAction("Update");
        }

    }
}
