using MintGarage.Models.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Products
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public double ProductPrice { get; set; }

        public int ProductQuantity { get; set; }

        public string ProductColor { get; set; }

        public string ProductDescription { get; set; }

        public string ProductType { get; set; }

        public string ProductMaterial { get; set; }

        public double ProductLength { get; set; }

        public double ProductWidth { get; set; }

        public double ProductHeight { get; set; }

        public string ProductImage { get; set; }


        [ForeignKey("Category")]
        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }
    }
}
