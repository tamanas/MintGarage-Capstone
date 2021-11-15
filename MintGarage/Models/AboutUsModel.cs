using MintGarage.Models.AboutUsT.TeamMembers;
using MintGarage.Models.AboutUsT.Values;
using System.Collections.Generic;

namespace MintGarage.Models
{
    public class AboutUsModel
    {
        public IEnumerable<TeamMember> TeamMembers { get; set; }
        public TeamMember TeamMember { get; set; }
        public IEnumerable<Value> Values { get; set; }
        public Value Value { get; set; }
    }
}
