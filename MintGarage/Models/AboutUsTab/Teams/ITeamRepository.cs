using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.AboutUsTab.Teams
{
     public interface ITeamRepository
    {
        IQueryable<Team> Teams { get; }
        void Add(Team team);
        void Delete(Team team);
        void Update(Team team);
        void Save();
    }
}
