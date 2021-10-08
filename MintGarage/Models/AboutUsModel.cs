using MintGarage.Models.AboutUsTab.Teams;
using MintGarage.Models.AboutUsTab.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models
{
    public class AboutUsModel
    {
        public IEnumerable<Team> Teams { get; set; }
        public Team Team { get; set; }
        public IEnumerable<Value> Values { get; set; }
        public Value Value { get; set; }
    }
}
