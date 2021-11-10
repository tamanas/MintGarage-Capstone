using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.AboutUsTab.Teams
{
    public class Team
    {

        [Key]
        public int MemberID { get; set; }

        [Display(Name = "Member Name")]
        [Required(ErrorMessage = "Enter member name")]
        public string MemberName { get; set; }

        [Display(Name = "Memeber Role")]
        [Required(ErrorMessage = "Enter memeber role")]
        public string MemberRole { get; set; }

        [Display(Name = "Member Image")]
        public string MemberImage { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
