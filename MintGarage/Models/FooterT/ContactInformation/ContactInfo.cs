using System.ComponentModel.DataAnnotations;

namespace MintGarage.Models.FooterT.ContactInformation
{
    public class ContactInfo
    {
        [Key]
        public int ContactInfoID { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Must be 10 digits")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
    }
}
