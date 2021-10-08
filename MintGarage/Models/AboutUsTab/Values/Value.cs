using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.AboutUsTab.Values
{
    public class Value
    {
        [Key]
        public int ValueID { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Enter value title")]
        public string ValueTitle { get; set; }

        [Display(Name = "Value Description")]
        [Required(ErrorMessage = "Enter value description")]
        public string ValueDescription { get; set; }

        [Display(Name = "Value Image")]
        public string ValueImage { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
