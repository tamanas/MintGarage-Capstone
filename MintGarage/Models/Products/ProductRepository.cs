using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Products
{
    public class ProductRepository : IProductRepository
    {
        private MintGarageContext context;
        public ProductRepository(MintGarageContext ctx)
        {
            context = ctx;           
        }
        public IQueryable<Product> Products => context.Product;
    }
}
