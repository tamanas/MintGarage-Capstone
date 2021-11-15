using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.AccountT
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Required(ErrorMessage="Enter username")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }
    }
}