using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.GalleryTab
{
    public class GalleryRepository : IRepository<Gallery>
    {
        private MintGarageContext context;
        public GalleryRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Gallery> Items => context.Gallery;

        public void Create(Gallery item)
        {
            context.Gallery.Add(item);
            Save();
        }
        public void Update(Gallery item)
        {
            context.Gallery.Update(item);
            Save();
        }
        public void Delete(Gallery item)
        {
            context.Gallery.Remove(item);
            Save();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
