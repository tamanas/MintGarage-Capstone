using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MintGarage.Database;

namespace MintGarage.Models.FooterContents.FooterSocialMedias
{
    public class FooterSocialMediaRepository : IFooterSocialMediaRepository
    {
        public MintGarageContext context;

        public FooterSocialMediaRepository(MintGarageContext ctx)
        {
            context = ctx;
        }

        public IQueryable<FooterSocialMedia> FooterSocialMedias => context.FooterSocialMedias;

        public void Create(FooterSocialMedia footerSocialMedia)
        {
            context.FooterSocialMedias.Add(footerSocialMedia);
            Save();
        }

        public void Edit(FooterSocialMedia footerSocialMedia)
        {
            context.FooterSocialMedias.Update(footerSocialMedia);
            Save();
        }

        public void Delete(FooterSocialMedia footerSocialMedia)
        {
            context.FooterSocialMedias.Remove(footerSocialMedia);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
