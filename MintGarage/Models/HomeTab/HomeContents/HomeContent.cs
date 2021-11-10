using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.HomeTab.HomeContents
{
    public class HomeContent
    {
        [Key]
        public int HomeContentsID { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title Required")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [NotMapped]
        [Display(Name = "upload Image")]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public string TableName { get; set; } = "HomeContent";
    }
}
