using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models
{
    public class Description
    {
        [Key]
        public int DescriptionID { get; set; }

        public string Color { get; set; }

        public string ProductDescription { get; set; }

        public string Type { get; set; }

        public string Material { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public virtual ICollection<Product> Product { get; set; }

    }
}
