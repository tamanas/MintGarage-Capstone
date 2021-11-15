using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.AccountT
{
    public class UpdatePassword
    {
        [Display (Name = "Current password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Current password is required")]
        public string CurrectPassword { set; get; }

        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "New password is required")]
        public string NewPassword { set; get; }

        [Display(Name = "Confirm new password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match. ")]
        [Required(ErrorMessage = "Confirm new password is required")]
        public string ConfirmPassword { set; get; }
    }
}
