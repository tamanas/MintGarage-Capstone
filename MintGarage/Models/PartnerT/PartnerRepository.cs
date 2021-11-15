using MintGarage.Database;
using System.Linq;

namespace MintGarage.Models.PartnerT
{
    public class PartnerRepository : IRepository<Partner>
    {
        public MintGarageContext context;
        public PartnerRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Partner> Items => context.Partner;

        public void Create(Partner item)
        {
            context.Partner.Add(item);
            Save();
        }

        public void Update(Partner item)
        {
            context.Partner.Update(item);
            Save();
        }

        public void Delete(Partner item)
        {
            context.Partner.Remove(item);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}
