using System.Linq;
using MintGarage.Database;

namespace MintGarage.Models.FooterT.SocialMedias
{
    public class SocialMediaRepository : IRepository<SocialMedia>
    {
        public MintGarageContext context;

        public SocialMediaRepository(MintGarageContext ctx)
        {
            context = ctx;
        }

        public IQueryable<SocialMedia> Items => context.SocialMedia;

        public void Create(SocialMedia item)
        {
            context.SocialMedia.Add(item);
            Save();
        }

        public void Update(SocialMedia item)
        {
            context.SocialMedia.Update(item);
            Save();
        }

        public void Delete(SocialMedia item)
        {
            context.SocialMedia.Remove(item);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}
