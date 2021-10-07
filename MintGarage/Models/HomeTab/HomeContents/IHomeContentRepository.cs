using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.HomeTab.HomeContents
{
    public interface IHomeContentRepository
    {
        IQueryable<HomeContent> HomeContents { get; }
        void AddHomeContents(HomeContent homeContents);
        void DeleteHomeContents(HomeContent homeContents);
        void UpdateHomeContents(HomeContent homeContents);
        void SaveHomeContents();
    }
}
