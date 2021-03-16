using Microsoft.AspNetCore.Mvc;
using MintGarage.Models;
using MintGarage.Models.Categories;
using MintGarage.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepo;
        private ICategoryRepository categoryRepo;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            productRepo = productRepository;
            categoryRepo = categoryRepository;
        }
        public IActionResult Index(string sortOrder, int filterID, string searchItem)
        {
            ViewBag.Categories = (IEnumerable<Category>)categoryRepo.Categories;
            var list = productRepo.Products;

            if(sortOrder != null) {
                switch (sortOrder)
                {
                    case "name_asc":
                        list = list.OrderBy(x => x.ProductName);
                        break;

                    case "name_desc":
                        list = list.OrderByDescending(x => x.ProductName);
                        break;

                    case "price_asc":
                        list = list.OrderBy(x => x.ProductPrice);
                        break;

                    case "price_desc":
                        list = list.OrderByDescending(x => x.ProductPrice);
                        break;
                }
            }

            if(filterID != 0)
            {
                list = list.Where(x => x.CategoryID == filterID);
            }

            if(searchItem != null)
            {
                list = list.Where(x => x.ProductName.Contains(searchItem));
            }


            return View(list);
        }
    }
}
