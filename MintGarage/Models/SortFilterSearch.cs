using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models
{
    public class SortFilterSearch
    {
        public string SortBy { get; set; }
        public int FilterID { get; set; }
        public string SearchValue { get; set; }
    }
}
