using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private MintGarageContext context;
        public CategoryRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Category> Cateogries => context.Category;
    }
}
