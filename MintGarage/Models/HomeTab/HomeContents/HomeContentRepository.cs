using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.HomeTab.HomeContents
{
    public class HomeContentRepository : IHomeContentRepository
    {
        private MintGarageContext context;
        public HomeContentRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<HomeContent> HomeContents => context.HomeContents;

        public void AddHomeContents(HomeContent homeContents)
        {
            context.HomeContents.Add(homeContents);
            SaveHomeContents();
        }

        public void DeleteHomeContents(HomeContent homeContents)
        {
            context.HomeContents.Remove(homeContents);
            SaveHomeContents();
        }

        public void UpdateHomeContents(HomeContent homeContents)
        {
            context.HomeContents.Update(homeContents);
            SaveHomeContents();
        }
        public void SaveHomeContents()
        {
            context.SaveChanges();
        }
    }
}

