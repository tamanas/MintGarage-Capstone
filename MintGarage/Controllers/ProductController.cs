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

        /*public IActionResult Index(string sortOrder, int filterID, string searchItem)
        {
            
            ViewBag.Categories = (IEnumerable<Category>)categoryRepo.Categories;
            var productList = productRepo.Products;
            var categoryList = categoryRepo.Categories;
           
         

                if (sortOrder != null)
                {
                    switch (sortOrder)
                    {
                        case "name_asc":
                            productList = productList.OrderBy(x => x.ProductName);
                            break;

                        case "name_desc":
                            productList = productList.OrderByDescending(x => x.ProductName);
                            break;

                        case "price_asc":
                            productList = productList.OrderBy(x => x.ProductPrice);
                            break;

                        case "price_desc":
                            productList = productList.OrderByDescending(x => x.ProductPrice);
                            break;
                    }
                }

                if (filterID != 0)
                {
                    productList = productList.Where(x => x.CategoryID == filterID);
                }

                if (searchItem != null)
                {
                    productList = productList.Where(x => x.ProductName.Contains(searchItem));
                }

            
            ProductCategory productCategory = new ProductCategory()
            {
                Products = productList,
                Categories = categoryList,
                SortFilterSearch = new SortFilterSearch()

            };


            return View(productCategory);
        }
*/
        public IActionResult Index(SortFilterSearch sortFilterSearch)
        {

            ViewBag.Categories = (IEnumerable<Category>)categoryRepo.Categories;
            var productList = productRepo.Products;
            var categoryList = categoryRepo.Categories;

            if (sortFilterSearch != null)
            {
                var sortOrder = sortFilterSearch.SortBy;

                var filterID = sortFilterSearch.FilterID;
                var searchItem = sortFilterSearch.SearchValue;


                if (sortOrder != null)
                {
                    switch (sortOrder)
                    {
                        case "name_asc":
                            productList = productList.OrderBy(x => x.ProductName);
                            break;

                        case "name_desc":
                            productList = productList.OrderByDescending(x => x.ProductName);
                            break;

                        case "price_asc":
                            productList = productList.OrderBy(x => x.ProductPrice);
                            break;

                        case "price_desc":
                            productList = productList.OrderByDescending(x => x.ProductPrice);
                            break;
                    }
                }

                if (filterID != 0)
                {
                    productList = productList.Where(x => x.CategoryID == filterID);
                }

                if (searchItem != null)
                {
                    productList = productList.Where(x => x.ProductName.Contains(searchItem));
                }

            }

            ProductCategory productCategory = new ProductCategory()
            {
                Products = productList,
                Categories = categoryList,
                SortFilterSearch = new SortFilterSearch()

            };


            return View(productCategory);
        }

/*        [HttpPost]
        public IActionResult Index(ProductCategory prodCategory)
        {

            var sortOrder = prodCategory.SortFilterSearch.SortBy;
            var filterID = prodCategory.SortFilterSearch.FilterID;
            var searchItem = prodCategory.SortFilterSearch.SearchValue;

            return RedirectToAction("Index", new SortFilterSearch() { SortBy = sortOrder, FilterID = filterID, SearchValue = searchItem });

        }*/
    }
}
