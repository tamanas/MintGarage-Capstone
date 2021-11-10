using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.GalleryTab
{
    public class GalleryRepository : IGalleryRepository
    {
        private MintGarageContext context;
        public GalleryRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Gallery> Galleries => context.Gallery;

        public void Add(Gallery gallery)
        {
            context.Gallery.Add(gallery);
            Save();
        }

        public void Delete(Gallery gallery)
        {
            context.Gallery.Remove(gallery);
            Save();
        }

        public void Update(Gallery gallery)
        {
            context.Gallery.Update(gallery);
            Save();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
