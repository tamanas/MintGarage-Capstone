using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using MintGarage.Models.HomeTab.Reviews;
using MintGarage.Models.HomeTab.HomeContents;
using MintGarage.Models.HomeTab.Suppliers;

namespace MintGarage.Models
{
    public class HomeModel
    {
        public IEnumerable<Review> Reviews { get; set; }
        public Review Review { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public Supplier Supplier { get; set; }
        public IEnumerable<HomeContent> HomeContents { get; set; }
        public HomeContent HomeContent { get; set; }

    }
}


