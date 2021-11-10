using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Partners
{
    public class Partner
    {
        [Key]
        public int PartnerID { get; set; }

        [Display(Name = "Partner Name")]
        [Required(ErrorMessage = "Partner Name required")]
        public string PartnerName { get; set; }

        [Display(Name = "Partner URL")]
        [Required(ErrorMessage = "Partner URL required")]
        public string PartnerUrl { get; set; }

    }
}
