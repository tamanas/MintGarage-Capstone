using MintGarage.Database;
using System.Linq;

namespace MintGarage.Models.AboutUsT.Values
{
    public class ValueRepository : IRepository<Value>
    {
        private MintGarageContext context;
        public ValueRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Value> Items => context.Value;

        public void Create(Value item)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Value value)
        {
            context.Value.Update(value);
            Save();
        }

        public void Delete(Value item)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    
    }
}
