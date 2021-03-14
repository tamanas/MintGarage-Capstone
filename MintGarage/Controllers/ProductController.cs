using Microsoft.AspNetCore.Mvc;
using MintGarage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepo;
        public ProductController(IProductRepository repo)
        {
            productRepo = repo;
        }
        public IActionResult Index()
        {
            return View(productRepo.Products);
        }
    }
}
