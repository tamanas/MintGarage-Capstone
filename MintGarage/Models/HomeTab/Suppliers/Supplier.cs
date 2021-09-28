using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Logo")]
        [Required(ErrorMessage = "Logo Required")]
        public string SupplierLogo { get; set; }
    }
}

