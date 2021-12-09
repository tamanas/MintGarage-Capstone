using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MintGarage.Models.HomeT.Cards
{
    public class Card
    {
        [Key]
        public int CardID { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title Required")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [NotMapped]
        [Display(Name = "upload Image")]
        public IFormFile ImageFile { get; set; }
    }
}
