using MintGarage.Models.FooterT.ContactInformation;
using MintGarage.Models.FooterT.SocialMedias;
using System.Collections.Generic;

namespace MintGarage.Models
{
    public class FooterModel
    {
        public IEnumerable<ContactInfo> ContactInfos { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public IEnumerable<SocialMedia> SocialMedias { get; set; }
        public SocialMedia SocialMedia { get; set; }
    }
}