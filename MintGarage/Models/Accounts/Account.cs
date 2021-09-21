using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Accounts
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Enter Username")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }
    }
}