using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.HomeTab.Suppliers
{
    public class SupplierRepository : ISupplierRepository
    {
        private MintGarageContext context;
        public SupplierRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Supplier> Suppliers => context.Suppliers;

        public void AddSuppliers(Supplier suppliers)
        {
            context.Suppliers.Add(suppliers);
            SaveSuppliers();
        }

        public void DeleteSuppliers(Supplier suppliers)
        {
            context.Suppliers.Remove(suppliers);
            SaveSuppliers();
        }

        public void UpdateSuppliers(Supplier suppliers)
        {
            context.Suppliers.Update(suppliers);
            SaveSuppliers();
        }
        public void SaveSuppliers()
        {
            context.SaveChanges();
        }
    }
}
