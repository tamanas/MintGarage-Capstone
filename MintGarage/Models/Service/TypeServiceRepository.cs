using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Service
{
    public class TypeServiceRepository : ITypeServiceRepository
    {
        private MintGarageContext context;
        public TypeServiceRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<TypeService> TypeServices => context.TypeService;
    }
}
