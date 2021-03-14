using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models
{
    public class Product
    {
        [Key]
        public int  ProductID { get; set; }

        public string ProductName { get; set; } 

        public int Quantity { get; set; }

        public double ProductPrice { get; set; }

        public virtual ICollection<Description> Description { get; set; }

        [ForeignKey("Description")]
        public int? DescriptionID { get; set; }
    }
}
