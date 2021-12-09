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
        public string ImageFile { get;  } = "~/Images/home/";
        public string TabImage { get; } = "construction.jpg";

        public string TabImageSlogon = "IT'S TIME TO MINT YOUR GARAGE!";
        public string CardTitle = "Garage Transformations and Other Storage Solutions";
        public string ReviewTitle { get; } = "Our Reviews";
        public string SuppllerTitle { get; } = "Trusted Suppliers";

    }
}


