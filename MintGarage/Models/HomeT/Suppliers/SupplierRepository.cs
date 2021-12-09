using MintGarage.Database;
using System.Linq;

namespace MintGarage.Models.HomeT.Suppliers
{
    public class SupplierRepository : IRepository<Supplier>
    {
        private MintGarageContext context;
        public SupplierRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Supplier> Items => context.Supplier;

        public void Create(Supplier item)
        {
            context.Supplier.Add(item);
            Save();
        }

        public void Delete(Supplier item)
        {
            context.Supplier.Remove(item);
            Save();
        }

        public void Update(Supplier item)
        {
            context.Supplier.Update(item);
            Save();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
