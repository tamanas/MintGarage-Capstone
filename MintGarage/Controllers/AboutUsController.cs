using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MintGarage.Models;
using MintGarage.Models.AboutUsTab.Teams;
using MintGarage.Models.AboutUsTab.Values;
using MintGarage.Models.FooterContents.FooterContactInfo;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using MintGarage.Models.HomeTab.HomeContents;
using MintGarage.Models.Partners;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using MintGarage.Models.FooterContents.FooterContactInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace MintGarage.Controllers
{
    public class AboutUsController : Controller
    {
        private IPartnerRepository partnerRepo;
        private IFooterContactInfoRepository footerContactInfoRepo;
        private IFooterSocialMediaRepository footerSocialMediaRepo;
        private ITeamRepository teamRepo;
        private IValueRepository valueRepo;

        private IWebHostEnvironment hostEnv;
        private string imageFolder = "/Images/";

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
        "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";

        public AboutUsController(IPartnerRepository partnerRepository, IFooterContactInfoRepository footerContactInfoRepository,
            IFooterSocialMediaRepository footerSocialMediaRepository, ITeamRepository teamRepository, IValueRepository valueRepository,
            IWebHostEnvironment hostEnvironment)
        {
            partnerRepo = partnerRepository;
            footerContactInfoRepo = footerContactInfoRepository;
            footerSocialMediaRepo = footerSocialMediaRepository;
            teamRepo = teamRepository;
            valueRepo = valueRepository;
            hostEnv = hostEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            AboutUsModel aboutUs = new AboutUsModel()
            
            {
                Teams = teamRepo.Teams,
                Values = valueRepo.Values,
            };
            return View(aboutUs);
        }

        public IActionResult Update(int? id, string? operation, bool? show, string? table)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;
            ViewBag.message = TempData["message"];

            setViewBag(false, false, false, "");

            if (operation != null && show != null && table != "")
            {
                ViewBag.table = table;
                switch (operation)
                {
                    case "add":
                        ViewBag.add = show;
                        break;
                    case "edit":
                        ViewBag.edit = show;
                        break;
                    case "delete":
                        ViewBag.delete = show;
                        break;
                }
            }

            AboutUsModel aboutUsModel = new AboutUsModel();
            aboutUsModel.Values = valueRepo.Values;
            aboutUsModel.Teams = teamRepo.Teams;

            if (id != null && operation != "add")
            {
                aboutUsModel.Value = valueRepo.Values.FirstOrDefault(s => s.ValueID == id);
                aboutUsModel.Team = teamRepo.Teams.FirstOrDefault(s => s.MemberID == id);
            }

            return View(aboutUsModel);
        }

        public async Task<IActionResult> EditValue(AboutUsModel aboutUsModel)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                if (aboutUsModel.Value.ImageFile != null)
                { 
                    DeleteImage(aboutUsModel.Value.ValueImage);
                    aboutUsModel.Value.ValueImage = await SaveImage(aboutUsModel.Value.ImageFile);
                }
                valueRepo.Update(aboutUsModel.Value);
                TempData["message"] = "Successfully edited Value.";
            }
            else
            {
                aboutUsModel.Values = valueRepo.Values;
                aboutUsModel.Teams = teamRepo.Teams;
                setViewBag(false, true, false, "value");
                return View("Update", aboutUsModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> AddMember(AboutUsModel aboutUsModel)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid && aboutUsModel.Team.ImageFile != null)
            {
                aboutUsModel.Team.MemberImage = await SaveImage(aboutUsModel.Team.ImageFile);
                teamRepo.Add(aboutUsModel.Team);
                TempData["message"] = "Successfully added Team Member.";
            }
            else
            {
                aboutUsModel.Values = valueRepo.Values;
                aboutUsModel.Teams = teamRepo.Teams;
                setViewBag(true, false, false, "team");
                return View("Update", aboutUsModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> EditMember(AboutUsModel aboutUsModel)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                if (aboutUsModel.Team.ImageFile != null)
                {
                    DeleteImage(aboutUsModel.Team.MemberImage);
                    aboutUsModel.Team.MemberImage = await SaveImage(aboutUsModel.Team.ImageFile);
                }
                teamRepo.Update(aboutUsModel.Team);
                TempData["message"] = "Successfully edited Team Member.";
            }
            else
            {
                aboutUsModel.Values = valueRepo.Values;
                aboutUsModel.Teams = teamRepo.Teams;
                setViewBag(false, true, false, "team");
                return View("Update", aboutUsModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult DeleteMember(AboutUsModel aboutUsModel)
        {
            ViewBag.Partners = partnerRepo.Partners;
            ViewBag.SocialMedias = footerSocialMediaRepo.FooterSocialMedias;
            ViewBag.Contacts = footerContactInfoRepo.FooterContactInfo;
            ViewBag.AboutData = AboutUs;

            DeleteImage(aboutUsModel.Team.MemberImage);
            teamRepo.Delete(aboutUsModel.Team);
            TempData["message"] = "Successfully deleted Team Member.";
            return RedirectToAction("Update");
        }

        public void setViewBag(bool add, bool edit, bool delete, string table)
        {
            ViewBag.add = add;
            ViewBag.edit = edit;
            ViewBag.delete = delete;
            ViewBag.table = table;
        }

        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = Path.GetFileNameWithoutExtension(imageFile.FileName) +
                                        DateTime.Now.ToString("yyMMddssffff") +
                                        Path.GetExtension(imageFile.FileName);
            string imagePath = Path.Combine(hostEnv.WebRootPath + imageFolder, imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        public void DeleteImage(string imageName)
        {
            string imagePath = Path.Combine(hostEnv.WebRootPath + imageFolder, imageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
    }
}
