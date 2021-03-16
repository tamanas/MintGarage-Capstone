using MintGarage.Models.Categories;
using MintGarage.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models
{
    public class ProductCategory
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public SortFilterSearch SortFilterSearch { get; set; } = new SortFilterSearch();
    }
}
