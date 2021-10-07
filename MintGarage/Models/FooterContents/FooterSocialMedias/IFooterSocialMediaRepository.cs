using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.FooterContents.FooterSocialMedias
{
    public interface IFooterSocialMediaRepository
    {
        IQueryable<FooterSocialMedia> FooterSocialMedias { get; }
        void Create(FooterSocialMedia footerSocialMedia);
        void Edit(FooterSocialMedia footerSocialMedia);
        void Delete(FooterSocialMedia footerSocialMedia);
        void Save();
    }
}
