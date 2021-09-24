using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Partners
{
    public class PartnerRepository : IPartnerRepository
    {
        private MintGarageContext context;
        public PartnerRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Partner> Partner => context.Partner;

        public void Create(Partner partner)
        {
            context.Partner.Add(partner);
            Save();
        }

        public void Edit(Partner partner)
        {
            context.Partner.Update(partner);
            Save();
        }

        public void Delete(Partner partner)
        {
            context.Partner.Remove(partner);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
