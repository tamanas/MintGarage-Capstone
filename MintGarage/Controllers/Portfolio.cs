﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Controllers
{
    public class Portfolio : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}