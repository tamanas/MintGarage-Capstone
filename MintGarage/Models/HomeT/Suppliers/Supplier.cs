using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MintGarage.Models.HomeT.Suppliers
{
    public class Supplier
    {
        [Key]
        public int SuppliersID { get; set; }

        [Display(Name = "Supplier Name")]
        [Required(ErrorMessage = "Supplier Name Required")]
        public string SuppliersName { get; set; }

        public string SupplierLogo { get; set; }

        [NotMapped]
        [Display(Name = "Upload Logo")]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public string TableName { get; set; } = "Supplier";
    }
}

