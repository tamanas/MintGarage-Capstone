using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.GalleryTab
{
    public interface IGalleryRepository
    {
        IQueryable<Gallery> Galleries { get; }
        void Add(Gallery gallery);
        void Delete(Gallery gallery);
        void Update(Gallery gallery);
        void Save();
    }
}
