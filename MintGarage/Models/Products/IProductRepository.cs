using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Products
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
