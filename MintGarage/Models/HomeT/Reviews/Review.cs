using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MintGarage.Models.HomeT.Reviews
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

        [NotMapped]
        public string TableName { get; set; } = "Review";
    }
}

