using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Service
{
    public interface ITypeServiceRepository
    {
        IQueryable<TypeService> TypeServices { get; }
    }
}
