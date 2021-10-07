using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MintGarage.Database;

namespace MintGarage.Models.FooterContents.FooterContactInfo
{
    public class FooterContactInfoRepository : IFooterContactInfoRepository
    {
        private MintGarageContext context;

        public FooterContactInfoRepository(MintGarageContext ctx)
        {
            context = ctx;
        }

        public IQueryable<FooterContactInfo> FooterContactInfo => context.FooterContactInfo;

        public void Update(FooterContactInfo contactInfo)
        {
                context.FooterContactInfo.Update(contactInfo);
                Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
