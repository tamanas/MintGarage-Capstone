using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.AboutUsTab.Values
{
    public class ValueRepository : IValueRepository
    {
        private MintGarageContext context;
        public ValueRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Value> Values => context.Value;

        public void Update(Value value)
        {
            context.Value.Update(value);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
