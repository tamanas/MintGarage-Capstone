using MintGarage.Models.GalleryT;
using System.Collections.Generic;

namespace MintGarage.Models
{
    public class GalleryModel
    {
        public IEnumerable<Gallery> Galleries { get; set; }
        public Gallery Gallery { get; set; }
    }
}
