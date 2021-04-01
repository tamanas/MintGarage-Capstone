using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace MintGarage.Models.ConsultationForms
{
    public class ConsultationForm
    {
        [Key]
        public int ConsultationFormID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Required")]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Must be 10 digits")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Service Type")]
        [Required(ErrorMessage = "Service Type Required")]
        public string ServiceType { get; set; }

        [Display(Name = "Description")]
        public string FormDescription { get; set; }

    }
}

