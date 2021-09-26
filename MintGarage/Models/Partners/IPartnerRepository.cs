using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Partners
{
    public interface IPartnerRepository
    {
        IQueryable<Partner> Partners { get; }

        void Create(Partner partner);
        void Edit(Partner partner);
        void Delete(Partner partner);
        void Save();
    }
}
