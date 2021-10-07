using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.HomeTab.Suppliers
{
    public class Supplier
    {
        [Key]
        public int SuppliersID { get; set; }

        [Display(Name = "Supplier Name")]
        [Required(ErrorMessage = "Supplier Name Required")]
        public string SuppliersName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public string SupplierLogo { get; set; }

        [NotMapped]
        [Display(Name = "Upload Logo")]
        [Required(ErrorMessage = "Logo Required")]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public string TableName { get; set; } = "Supplier";
    }
}

