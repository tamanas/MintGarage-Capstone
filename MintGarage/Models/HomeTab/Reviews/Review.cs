using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.HomeTab.Reviews
{
    public class Review
    {
        [Key]
        public int ReviewsID { get; set; }

        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "Customer Name Required")]
        public string CustomerName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }

    }
}

