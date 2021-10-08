using Microsoft.AspNetCore.Mvc;
using MintGarage.Models.AboutUsTab.Teams;
using MintGarage.Models.AboutUsTab.Values;
using MintGarage.Models.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Controllers
{
    public class AboutUsController : Controller
    {

        private ITeamRepository teamRepo;
        private IValueRepository valueRepo;

        public AboutUsController(ITeamRepository teamRepository, IValueRepository valueRepository)
        {
            teamRepo = teamRepository;
            valueRepo = valueRepository;
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
