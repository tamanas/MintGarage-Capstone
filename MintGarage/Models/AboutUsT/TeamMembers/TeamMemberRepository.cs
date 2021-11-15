using MintGarage.Database;
using System.Linq;

namespace MintGarage.Models.AboutUsT.TeamMembers
{
    public class TeamMemberRepository : IRepository<TeamMember>
    {
        private MintGarageContext context;
        public TeamMemberRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<TeamMember> Items => context.TeamMember;

        public void Create(TeamMember item)
        {
            context.TeamMember.Add(item);
            Save();
        }

        public void Delete(TeamMember item)
        {
            context.TeamMember.Remove(item);
            Save();
        }

        public void Update(TeamMember item)
        {
            context.TeamMember.Update(item);
            Save();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
