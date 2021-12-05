using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MintGarage.Models.FooterContents.FooterContactInfo
{
    public class FooterContactInfo
    {
        [Key]
        public int FooterContactInfoID { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Must be 10 digits")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }

        [Display(Name = "Working Hour")]
        [Required(ErrorMessage = "Working Hour Required")]
        public string WorkingHour { get; set; }
    }
}
