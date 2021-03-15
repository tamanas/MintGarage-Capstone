using MintGarage.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Categories
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        public string Name { get; set; }

        [ForeignKey("CategoryID")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
