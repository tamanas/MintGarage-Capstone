using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MintGarage.Models.HomeTab.SocialMedias;
using MintGarage.Models.HomeTab.Contacts;
using MintGarage.Models;

namespace MintGarage.Controllers
{
    public class FooterController : Controller
    {

        private ISocialMediaRepository socialContentRepo;
        private IContactRepository contactRepo;

        public FooterController(ISocialMediaRepository socialRepository, IContactRepository contactRepository)
        {
            socialContentRepo = socialRepository;
            contactRepo = contactRepository;
        }
        public IActionResult Index()
        {
            var socialList = socialContentRepo.SocialMedias;
            var contactList = contactRepo.Contacts;

            FooterModel footerModel = new FooterModel()
            {
                SocialMedia = socialList,
                Contact = contactList,
            };

            return View(footerModel);
        }
    }
}

