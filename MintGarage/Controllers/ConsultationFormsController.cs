using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MintGarage.Database;
using MintGarage.Models.ConsultationForms;

namespace MintGarage.Controllers
{
    public class ConsultationFormsController : Controller
    {
        public  IConsultationFormRepository consultationRepository;

        public ConsultationFormsController(IConsultationFormRepository consultationRepo)
        {
            consultationRepository = consultationRepo;
        }

        // GET: ConsultationForms
        public async Task<IActionResult> Index()
        {
            return View(await consultationRepository.ConsultationForms.ToListAsync());
        }

        // GET: ConsultationForms/Create
        public IActionResult Create()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Success = TempData["Success"];
            return View();
        }

        // POST: ConsultationForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            return View(consultationForm);
        }
    }
}
