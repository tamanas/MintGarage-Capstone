using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.GalleryTab
{
    public class Gallery
    {
        [Key]
        public int GalleryID { get; set; }

        [Display(Name = "Before Image")]
        public string BeforeImage { get; set; }

        [Display(Name = "After Image")]
        public string AfterImage { get; set; }

        [NotMapped]
        public IFormFile BeforeImageFile { get; set; }

        [NotMapped]
        public IFormFile AfterImageFile { get; set; }
    }
}
