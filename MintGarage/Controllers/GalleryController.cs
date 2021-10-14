using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MintGarage.Database;
using MintGarage.Models;
using MintGarage.Models.GalleryTab;
using MintGarage.Models.Partners;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using MintGarage.Models.FooterContents.FooterContactInfo;

namespace MintGarage.Controllers
{
    public class GalleryController : Controller
    {
        private IGalleryRepository galleryRepo;
        public IPartnerRepository partnerRepo;
        private IFooterContactInfoRepository footerContactInfoRepo;
        private IFooterSocialMediaRepository footerSocialMediaRepo;

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
            "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";
        private IWebHostEnvironment hostEnv;
        private string imageFolder = "/Images/";

        private readonly MintGarageContext _context;

        public GalleryController(IGalleryRepository galleryRepository, IPartnerRepository partnerRepository, IFooterContactInfoRepository footerContactInfoRepository, IFooterSocialMediaRepository footerSocialMediaRepository, IWebHostEnvironment hostEnvironment, MintGarageContext context)
        {
            galleryRepo = galleryRepository;
            partnerRepo = partnerRepository;
            footerContactInfoRepo = footerContactInfoRepository;
            footerSocialMediaRepo = footerSocialMediaRepository;
            hostEnv = hostEnvironment;
            _context = context;
        }

        // GET: Galleries
        public async Task<IActionResult> Index()
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            return View(await _context.Gallery.ToListAsync());
        }

        // GET: Galleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Gallery
                .FirstOrDefaultAsync(m => m.GalleryID == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // GET: Galleries/Create
        public IActionResult Create()
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            return View();
        }

        // POST: Galleries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GalleryID,BeforeImage,AfterImage")] Gallery gallery)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                _context.Add(gallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gallery);
        }

        // GET: Galleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Gallery.FindAsync(id);
            if (gallery == null)
            {
                return NotFound();
            }
            return View(gallery);
        }

        // POST: Galleries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GalleryID,BeforeImage,AfterImage")] Gallery gallery)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (id != gallery.GalleryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gallery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryExists(gallery.GalleryID))
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
            return View(gallery);
        }

        // GET: Galleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Gallery
                .FirstOrDefaultAsync(m => m.GalleryID == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // POST: Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            var gallery = await _context.Gallery.FindAsync(id);
            _context.Gallery.Remove(gallery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalleryExists(int id)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            return _context.Gallery.Any(e => e.GalleryID == id);
        }
    }
}
