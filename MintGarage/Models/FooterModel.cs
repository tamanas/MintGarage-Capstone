using MintGarage.Models.FooterContents.FooterContactInfo;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models
{
    public class FooterModel
    {
        public IEnumerable<FooterContactInfo> FooterContactInfo { get; set; }
        public FooterContactInfo FooterContact { get; set; }
        public IEnumerable<FooterSocialMedia> FooterSocialMedias { get; set; }
        public FooterSocialMedia FooterSocialMedia { get; set; }
    }
}