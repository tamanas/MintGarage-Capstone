using MintGarage.Models.GalleryTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models
{
    public class GalleryModel
    {
        public IEnumerable<Gallery> Galleries { get; set; }
        public Gallery Gallery { get; set; }
    }
}
