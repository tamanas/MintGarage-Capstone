using MintGarage.Models.GalleryT;
using System.Collections.Generic;

namespace MintGarage.Models
{
    public class GalleryModel
    {
        public IEnumerable<Gallery> Galleries { get; set; }
        public Gallery Gallery { get; set; }
        public string ImageFile { get; } = "~/Images/gallery/";
        public string TabImage { get; } = "construction.jpg";
        public string TabImageSlogon = "STORAGE WITH A DIFFERENCE!";
        public string GalleryTitle { get; } = "INSPIRED BY MODERN LIVING & CUTTING-EDGE CONSTRUCTION TECHNOLOGIES";
    }
}
