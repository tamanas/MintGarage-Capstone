using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MintGarage.Database;
using MintGarage.Models.ConsultationForms;
using MintGarage.Models.Partners;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using MintGarage.Models.FooterContents.FooterContactInfo;

namespace MintGarage.Controllers
{
    public class ConsultationFormsController : Controller
    {
        public IConsultationFormRepository consultationRepository;
        public IPartnerRepository partnerRepository;
        private IFooterContactInfoRepository footerContactInfoRepository;
        private IFooterSocialMediaRepository footerSocialMediaRepository;

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
        "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";

        public ConsultationFormsController(IConsultationFormRepository consultationRepo, IPartnerRepository partnerRepo,
            IFooterContactInfoRepository footerContactInfoRepo, IFooterSocialMediaRepository footerSocialMediaRepo)
        {
            consultationRepository = consultationRepo;
            partnerRepository = partnerRepo;
            footerContactInfoRepository = footerContactInfoRepo;
            footerSocialMediaRepository = footerSocialMediaRepo;
        }

        public async Task<IActionResult> Update(string sortCol, bool sort, string searchString)
        {
            var forms = consultationRepository.ConsultationForms;
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            ViewData["CurrentFilter"] = searchString;

            // Serach Function
            if (!String.IsNullOrEmpty(searchString))
            {
                forms = forms.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString)
                                       || s.LastName.Contains(searchString)
                                       || s.EmailAddress.Contains(searchString)
                                       || s.PhoneNumber.Contains(searchString)
                                       || s.FormDescription.Contains(searchString) );
            }
      
            ViewData["allow"] = !sort;

            // Sort Function
            switch (sortCol)
            {
                case "fname":
                    if(sort) forms = forms.OrderByDescending(s => s.FirstName);
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
                    if(sort) forms = forms.OrderByDescending(s => s.ServiceType);
                    else forms = forms.OrderBy(s => s.ServiceType);
                    break;
                case "description":
                    if(sort) forms = forms.OrderByDescending(s => s.FormDescription);
                    else forms = forms.OrderBy(s => s.FormDescription);
                    break;
                default:
                    forms = forms.OrderBy(s => s.FirstName);
                    break;
            }
            return View(await forms.AsNoTracking().ToListAsync());
        }


        // GET: ConsultationForms/Create
        public IActionResult Create()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Success = TempData["Success"];
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            return View();
        }

        // POST: ConsultationForms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ConsultationFormID," +
            "FirstName,LastName,EmailAddress,FormDescription,PhoneNumber," +
            "ServiceType")] ConsultationForm consultationForm)
        {
            if (ModelState.IsValid)
            {
                new Email(consultationForm).SendEmail();
                try
                {
                    consultationRepository.AddConsultationForm(consultationForm);
                    TempData["Message"] = "Your consultation request has been sent successfully.";
                    TempData["Success"] = true;
                }
                catch (Exception e)
                {
                    TempData["Message"] = "Unable to send consultation request.";
                    TempData["Success"] = false;
                }
                return RedirectToAction("Create");
            } else
            {
                TempData["Message"] = "";
                TempData["Success"] = false;
            }
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            return View(consultationForm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var consultationForm = await consultationRepository.ConsultationForms
                .FirstOrDefaultAsync(m => m.ConsultationFormID == id);
            if (consultationForm == null)
            {
                return NotFound();
            }
            consultationRepository.DeleteConsultationForm(consultationForm);
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            return RedirectToAction("Update");
        }

    }
}
