using System.Collections.Generic;
using MintGarage.Models.HomeT.Reviews;
using MintGarage.Models.HomeT.Cards;
using MintGarage.Models.HomeT.Suppliers;

namespace MintGarage.Models
{
    public class HomeModel
    {
        public IEnumerable<Review> Reviews { get; set; }
        public Review Review { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public Supplier Supplier { get; set; }
        public IEnumerable<Card> Cards { get; set; }
        public Card Card { get; set; }

    }
}


