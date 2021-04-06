﻿using System;
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
        private readonly MintGarageContext _context;
        public  IConsultationFormRepository consultationRepository;

        public ConsultationFormsController(IConsultationFormRepository consultationRepo, MintGarageContext context)
        {
            _context = context;
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
            return View();
        }

        // POST: ConsultationForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultationFormID,FirstName,LastName,EmailAddress,FormDescription,PhoneNumber,ServiceType")] ConsultationForm consultationForm)
        {
            if (ModelState.IsValid)
            {
                new EmailController(consultationForm).SendEmail();
                consultationRepository.AddConsultationForm(consultationForm);
                return RedirectToAction(nameof(Index));
            }
            return View(consultationForm);
        }

        private bool ConsultationFormExists(int id)
        {
            return _context.ConsultationForm.Any(e => e.ConsultationFormID == id);
        }
    }
}
