using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MintGarage.Models.ConsultationT;
using MintGarage.Models.PartnerT;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using MintGarage.Models.FooterContents.FooterContactInfo;
using MintGarage.Models;

namespace MintGarage.Controllers
{
    public class ConsultationController : Controller
    {
        public IRepository<Consultation> consultationRepo;
        public IRepository<Partner> partnerRepo;
        private IFooterContactInfoRepository footerContactInfoRepository;
        private IFooterSocialMediaRepository footerSocialMediaRepository;

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
        "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";

        public ConsultationController(IRepository<Consultation> consultationRepository, IRepository<Partner> partnerRepository,
            IFooterContactInfoRepository footerContactInfoRepo, IFooterSocialMediaRepository footerSocialMediaRepo)
        {
            consultationRepo = consultationRepository;
            partnerRepo = partnerRepository;
            footerContactInfoRepository = footerContactInfoRepo;
            footerSocialMediaRepository = footerSocialMediaRepo;
        }

        public async Task<IActionResult> Update(string sortOrder, string searchString)
        {
            var forms = consultationRepo.Items;
            ViewBag.Partners = partnerRepo.Items;
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
            ViewData["FirstNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "fname_desc" : "fname_asc";
            ViewData["LastNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "lname_desc" : "lname_asc";
            ViewData["EmailSortParm"] = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "email_asc";
            ViewData["ServiceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "service_desc" : "service-asc";
            ViewData["DescSortParm"] = String.IsNullOrEmpty(sortOrder) ? "desc_desc" : "desc_asc";


            // Sort Function
            switch (sortOrder)
            {
                case "fname_desc":
                    forms = forms.OrderByDescending(s => s.FirstName);
                    break;
                case "fname_asc":
                    forms = forms.OrderBy(s => s.FirstName);
                    break;
                case "lname_desc":
                    forms = forms.OrderByDescending(s => s.LastName);
                    break;
                case "lname_asc":
                    forms = forms.OrderBy(s => s.LastName);
                    break;
                case "email_desc":
                    forms = forms.OrderByDescending(s => s.EmailAddress);
                    break;
                case "email_asc":
                    forms = forms.OrderBy(s => s.EmailAddress);
                    break;
                case "service_desc":
                    forms = forms.OrderByDescending(s => s.ServiceType);
                    break;
                case "service_asc":
                    forms = forms.OrderBy(s => s.ServiceType);
                    break;
                case "desc_desc":
                    forms = forms.OrderByDescending(s => s.FormDescription);
                    break;
                case "desc_asc":
                    forms = forms.OrderBy(s => s.FormDescription);
                    break;
                default:
                    break;
            }
            return View(await forms.AsNoTracking().ToListAsync());
        }


        // GET: ConsultationForms/Index
        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Success = TempData["Success"];
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            return View();
        }

        // POST: ConsultationForms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ConsultationID," +
            "FirstName,LastName,EmailAddress,FormDescription,PhoneNumber," +
            "ServiceType")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                new Email(consultation).SendEmail();
                try
                {
                    consultationRepo.Create(consultation);
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
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            return View(consultation);
        }

        public async Task<IActionResult> Delete(int? id)
        {
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
            ViewBag.SocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepository.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            return RedirectToAction("Update");
        }

    }
}
