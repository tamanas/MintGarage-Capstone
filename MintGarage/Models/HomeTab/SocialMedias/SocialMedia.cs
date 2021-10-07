using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.HomeTab.SocialMedias
{
    public class SocialMedia
    {
        [Key]
        public int MediaID { get; set; }

        [Display(Name = "Social Media Name")]
        [Required(ErrorMessage = "Social Required")]
        public string MediaName { get; set; }

        [Display(Name = "Link")]
        [Required(ErrorMessage = "Link Required")]
        [Url(ErrorMessage = "Not A Valid URL")]
        public string MediaLink { get; set; }

        [Display(Name = "Social Media Image")]
        [Required(ErrorMessage = "Media Picture Required")]
        public string MediaImage { get; set; }
    }
}

