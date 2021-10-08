using Microsoft.AspNetCore.Mvc;
using MintGarage.Models.GalleryTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Controllers
{
    public class GalleryController : Controller
    {
        private IGalleryRepository galleryRepo;

        public GalleryController(IGalleryRepository galleryRepository)
        {
            galleryRepo = galleryRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }
    }
}
