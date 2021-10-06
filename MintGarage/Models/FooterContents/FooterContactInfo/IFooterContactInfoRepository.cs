using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.FooterContents.FooterContactInfo
{
    public interface IFooterContactInfoRepository
    {
        IQueryable<FooterContactInfo> FooterContactInfo { get; }

        void Update(FooterContactInfo contactInfo);
    }
}
