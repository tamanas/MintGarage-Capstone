using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MintGarage.Models;
using MintGarage.Models.AboutUsT.TeamMembers;
using MintGarage.Models.AboutUsT.Values;
using MintGarage.Models.FooterT.ContactInformation;
using MintGarage.Models.FooterT.SocialMedias;
using MintGarage.Models.PartnerT;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace MintGarage.Controllers
{
    public class AboutUsController : Controller
    {
        private IRepository<Partner> partnerRepo;
        private IRepository<ContactInfo> contactInfoRepo;
        private IRepository<SocialMedia> socialMediaRepo;
        private IRepository<TeamMember> teamMemberRepo;
        private IRepository<Value> valueRepo;

        private IWebHostEnvironment hostEnv;
        private string imageFolder = "/Images/aboutus/";

        private const String AboutUs = "We are specialists in transforming and organizing any room. " +
        "We take pride in delivering outstanding quality and unique designs for our clients Across Canada & North America.";

        public AboutUsController(IRepository<Partner> partnerRepository, IRepository<ContactInfo> contactRepository,
            IRepository<SocialMedia> mediaRepository, IRepository<TeamMember> teamMemberRepository, IRepository<Value> valueRepository,
            IWebHostEnvironment hostEnvironment)
        {
            partnerRepo = partnerRepository;
            contactInfoRepo = contactRepository;
            socialMediaRepo = mediaRepository;
            teamMemberRepo = teamMemberRepository;
            valueRepo = valueRepository;
            hostEnv = hostEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;
            //HttpContext.Session.SetString("isAdminLoggedIn", "false");

            AboutUsModel aboutUs = new AboutUsModel()
            
            {
                TeamMembers = teamMemberRepo.Items,
                Values = valueRepo.Items,
            };
            return View(aboutUs);
        }

        public IActionResult Update(int? id, string? operation, bool? show, string? table)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
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
            aboutUsModel.Values = valueRepo.Items;
            aboutUsModel.TeamMembers = teamMemberRepo.Items;

            if (id != null && operation != "add")
            {
                aboutUsModel.Value = valueRepo.Items.FirstOrDefault(s => s.ValueID == id);
                aboutUsModel.TeamMember = teamMemberRepo.Items.FirstOrDefault(s => s.MemberID == id);
            }

            return View(aboutUsModel);
        }

        public async Task<IActionResult> EditValue(AboutUsModel aboutUsModel)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
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
                aboutUsModel.Values = valueRepo.Items;
                aboutUsModel.TeamMembers = teamMemberRepo.Items;
                setViewBag(false, true, false, "value");
                return View("Update", aboutUsModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> AddMember(AboutUsModel aboutUsModel)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid && aboutUsModel.TeamMember.ImageFile != null)
            {
                aboutUsModel.TeamMember.MemberImage = await SaveImage(aboutUsModel.TeamMember.ImageFile);
                teamMemberRepo.Create(aboutUsModel.TeamMember);
                TempData["message"] = "Successfully added Team Member.";
            }
            else
            {
                aboutUsModel.Values = valueRepo.Items;
                aboutUsModel.TeamMembers = teamMemberRepo.Items;
                setViewBag(true, false, false, "team");
                return View("Update", aboutUsModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> EditMember(AboutUsModel aboutUsModel)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            if (ModelState.IsValid)
            {
                if (aboutUsModel.TeamMember.ImageFile != null)
                {
                    DeleteImage(aboutUsModel.TeamMember.MemberImage);
                    aboutUsModel.TeamMember.MemberImage = await SaveImage(aboutUsModel.TeamMember.ImageFile);
                }
                teamMemberRepo.Update(aboutUsModel.TeamMember);
                TempData["message"] = "Successfully edited Team Member.";
            }
            else
            {
                aboutUsModel.Values = valueRepo.Items;
                aboutUsModel.TeamMembers = teamMemberRepo.Items;
                setViewBag(false, true, false, "team");
                return View("Update", aboutUsModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult DeleteMember(AboutUsModel aboutUsModel)
        {
            if (HttpContext.Session.GetString("isAdminLoggedIn").Equals("false"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Partners = partnerRepo.Items;
            ViewBag.SocialMedias = socialMediaRepo.Items;
            ViewBag.Contacts = contactInfoRepo.Items;
            ViewBag.AboutData = AboutUs;

            DeleteImage(aboutUsModel.TeamMember.MemberImage);
            teamMemberRepo.Delete(aboutUsModel.TeamMember);
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
