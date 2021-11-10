using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.AboutUsTab.Teams
{
    public class TeamRepository : ITeamRepository
    {
        private MintGarageContext context;
        public TeamRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Team> Teams => context.Team;

        public void Add(Team team)
        {
            context.Team.Add(team);
            Save();
        }

        public void Delete(Team team)
        {
            context.Team.Remove(team);
            Save();
        }

        public void Update(Team team)
        {
            context.Team.Update(team);
            Save();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
