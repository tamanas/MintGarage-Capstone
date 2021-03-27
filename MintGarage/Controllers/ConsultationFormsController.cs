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
        private readonly MintGarageContext _context;

        public ConsultationFormsController(MintGarageContext context)
        {
            _context = context;
        }

        // GET: ConsultationForms
        public async Task<IActionResult> Index()
        {
            var mintGarageContext = _context.ConsultationForm.Include(c => c.TypeService);
            return View(await mintGarageContext.ToListAsync());
        }

        // GET: ConsultationForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultationForm = await _context.ConsultationForm
                .Include(c => c.TypeService)
                .FirstOrDefaultAsync(m => m.ConsultationFormID == id);
            if (consultationForm == null)
            {
                return NotFound();
            }

            return View(consultationForm);
        }

        // GET: ConsultationForms/Create
        public IActionResult Create()
        {
            ViewData["ServiceID"] = new SelectList(_context.TypeService, "ServiceID", "ServiceID");
            return View();
        }

        // POST: ConsultationForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultationFormID,FirstName,LastName,EmailAddress,FormDescription,PhoneNumber,ServiceID")] ConsultationForm consultationForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultationForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceID"] = new SelectList(_context.TypeService, "ServiceID", "ServiceID", consultationForm.ServiceID);
            return View(consultationForm);
        }

        // GET: ConsultationForms/Edit/5
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
            ViewData["ServiceID"] = new SelectList(_context.TypeService, "ServiceID", "ServiceID", consultationForm.ServiceID);
            return View(consultationForm);
        }

        // POST: ConsultationForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultationFormID,FirstName,LastName,EmailAddress,FormDescription,PhoneNumber,ServiceID")] ConsultationForm consultationForm)
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
            ViewData["ServiceID"] = new SelectList(_context.TypeService, "ServiceID", "ServiceID", consultationForm.ServiceID);
            return View(consultationForm);
        }

        // GET: ConsultationForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultationForm = await _context.ConsultationForm
                .Include(c => c.TypeService)
                .FirstOrDefaultAsync(m => m.ConsultationFormID == id);
            if (consultationForm == null)
            {
                return NotFound();
            }

            return View(consultationForm);
        }

        // POST: ConsultationForms/Delete/5
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
