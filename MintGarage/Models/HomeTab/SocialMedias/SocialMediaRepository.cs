using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.HomeTab.SocialMedias
{
    public class SocialMediaRepository : ISocialMediaRepository
    {
        private MintGarageContext context;
        public SocialMediaRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<SocialMedia> SocialMedias => context.SocialMedias;

        public void AddSocialMedias(SocialMedia socialMedias)
        {
            context.SocialMedias.Add(socialMedias);
            SaveSocialMedias();
        }

        public void DeleteSocialMedias(SocialMedia socialMedias)
        {
            context.SocialMedias.Remove(socialMedias);
            SaveSocialMedias();
        }

        public void UpdateSocialMedias(SocialMedia socialMedias)
        {
            context.SocialMedias.Update(socialMedias);
            SaveSocialMedias();
        }

        public void SaveSocialMedias()
        {
            context.SaveChanges();
        }
    }
}
