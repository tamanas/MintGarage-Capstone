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

        [Display(Name = "Social Media Icon")]
        [Required(ErrorMessage = "Social Media Icon name required")]
        public string SocialMediaIcon { get; set; }

        [Display(Name = "Social Media Link")]
        [Required(ErrorMessage = "Social Media URL required")]
        [Url(ErrorMessage = "Invalid URL")]
        public string SocialMediaUrl { get; set; }
    }
}
