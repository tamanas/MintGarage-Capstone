using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Categories
{
    public class ICategoryRepository
    {
        public IQueryable<Category> Categories { get; }
    }
}
