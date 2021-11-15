using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MintGarage.Models.FooterT.SocialMedias
{
    public class SocialMedia
    {
        [Key]
        public int SocialMediaID { get; set; }

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
        public IFormFile ImageFile { get; set; }
    }
}
