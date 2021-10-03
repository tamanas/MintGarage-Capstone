using MintGarage.Models.HomeTab.Contacts;
using MintGarage.Models.HomeTab.SocialMedias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models
{
    public class FooterModel
    {
        public IEnumerable<Contact> Contact { get; set; }
        public IEnumerable<SocialMedia> SocialMedia { get; set; }
    }
}