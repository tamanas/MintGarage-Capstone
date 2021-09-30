using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Partners
{
    public class PartnerUpdateView
    {
        public IEnumerable<Partner> Partners { get; set; }
        public Partner Partner { get; set; }
    }
}
