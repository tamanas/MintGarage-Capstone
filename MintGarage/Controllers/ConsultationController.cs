using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MintGarage.Models.ConsultationT;
using MintGarage.Models.PartnerT;
using MintGarage.Models.FooterT.SocialMedias;
using MintGarage.Models.FooterT.ContactInformation;
using MintGarage.Models;

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
            var forms = consultationRepo.Items;
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
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
            return View(consultationModel);
        }

        // POST: ConsultationForms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ConsultationModel consultationModel)
        {
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
            return View(consultationModel.Consultation);
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
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            return RedirectToAction("Update");
        }

    }
}
