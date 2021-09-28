using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.HomeTab.SocialMedias
{
    public interface ISocialMediaRepository
    {
        IQueryable<SocialMedia> SocialMedias { get; }
        void AddSocialMedias(SocialMedia socialMedias);
        void DeleteSocialMedias(SocialMedia socialMedias);
        void UpdateSocialMedias(SocialMedia socialMedias);
        void SaveSocialMedias();
    }
}
