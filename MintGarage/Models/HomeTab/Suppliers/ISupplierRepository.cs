using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.HomeTab.Suppliers
{
    public interface ISupplierRepository
    {
        IQueryable<Supplier> Suppliers { get; }
        void AddSuppliers(Supplier suppliers);
        void DeleteSuppliers(Supplier suppliers);
        void UpdateSuppliers(Supplier suppliers);

        void SaveSuppliers();
    }
}
