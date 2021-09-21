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
    public class ConsultationForms1Controller : Controller
    {
        private readonly MintGarageContext _context;

        public ConsultationForms1Controller(MintGarageContext context)
        {
            _context = context;
        }

        // GET: ConsultationForms1
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["FirstNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "fname_desc" : "";
            ViewData["LastNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "lname_desc" : "";
            ViewData["EmailSortParm"] = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            ViewData["ServiceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "service_desc" : "";
            ViewData["DescSortParm"] = String.IsNullOrEmpty(sortOrder) ? "desc_desc" : "";

            ViewData["CurrentFilter"] = searchString;

            var forms = from s in _context.ConsultationForm
                           select s;

            // Serach Function
            if (!String.IsNullOrEmpty(searchString))
            {
                forms = forms.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString)
                                       || s.LastName.Contains(searchString)
                                       || s.EmailAddress.Contains(searchString)
                                       || s.PhoneNumber.Contains(searchString)
                                       || s.FormDescription.Contains(searchString)
                                       );
            }

            // Sort Function
            switch (sortOrder)
            {
                case "fname_desc":
                    forms = forms.OrderByDescending(s => s.FirstName);
                    break;
                case "lname_desc":
                    forms = forms.OrderByDescending(s => s.LastName);
                    break;
                case "email_desc":
                    forms = forms.OrderByDescending(s => s.EmailAddress);
                    break;
                case "service_desc":
                    forms = forms.OrderByDescending(s => s.ServiceType);
                    break;
                case "desc_desc":
                    forms = forms.OrderByDescending(s => s.FormDescription);
                    break;
                default:
                    forms = forms.OrderBy(s => s.FirstName);
                    forms = forms.OrderBy(s => s.LastName);
                    forms = forms.OrderBy(s => s.EmailAddress);
                    forms = forms.OrderBy(s => s.ServiceType);
                    forms = forms.OrderBy(s => s.FormDescription);
                    break;
            }
            //return View(await _context.ConsultationForm.ToListAsync());
            return View(await forms.AsNoTracking().ToListAsync());
        }

        // GET: ConsultationForms1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultationForm = await _context.ConsultationForm
                .FirstOrDefaultAsync(m => m.ConsultationFormID == id);
            if (consultationForm == null)
            {
                return NotFound();
            }

            return View(consultationForm);
        }

        // GET: ConsultationForms1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConsultationForms1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultationFormID,FirstName,LastName,EmailAddress,PhoneNumber,ServiceType,FormDescription")] ConsultationForm consultationForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultationForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consultationForm);
        }

        // GET: ConsultationForms1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultationForm = await _context.ConsultationForm.FindAsync(id);
            if (consultationForm == null)
            {
                return NotFound();
            }
            return View(consultationForm);
        }

        // POST: ConsultationForms1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultationFormID,FirstName,LastName,EmailAddress,PhoneNumber,ServiceType,FormDescription")] ConsultationForm consultationForm)
        {
            if (id != consultationForm.ConsultationFormID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultationForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultationFormExists(consultationForm.ConsultationFormID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(consultationForm);
        }

        // GET: ConsultationForms1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultationForm = await _context.ConsultationForm
                .FirstOrDefaultAsync(m => m.ConsultationFormID == id);
            if (consultationForm == null)
            {
                return NotFound();
            }

            return View(consultationForm);
        }

        // POST: ConsultationForms1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultationForm = await _context.ConsultationForm.FindAsync(id);
            _context.ConsultationForm.Remove(consultationForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultationFormExists(int id)
        {
            return _context.ConsultationForm.Any(e => e.ConsultationFormID == id);
        }
    }
}
