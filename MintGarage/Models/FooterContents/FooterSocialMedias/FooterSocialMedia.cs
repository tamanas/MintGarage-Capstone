using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.FooterContents.FooterSocialMedias
{
    public class FooterSocialMedia
    {
        [Key]
        public int FooterSocialMediaID { get; set; }

        [Display(Name = "Social Media Name")]
        [Required(ErrorMessage = "Social Media Name required")]
        public string Name { get; set; }

        [Display(Name = "Social Media Logo Path")]
        public string SocialMediaLogo { get; set; }

        [Display(Name = "Social Media Link")]
        [Required(ErrorMessage = "Social Media URL required")]
        [Url(ErrorMessage = "Invalid URL")]
        public string SocialMediaUrl { get; set; }

        [NotMapped]
        [Display(Name = "Social Media Logo Image")]
        //[Required(ErrorMessage = "Social Media Icon Required")]
        public IFormFile ImageFile { get; set; }
    }
}
