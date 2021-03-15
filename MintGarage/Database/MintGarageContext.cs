﻿using Microsoft.EntityFrameworkCore;
using MintGarage.Models;
using MintGarage.Models.Categories;
using MintGarage.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Database
{
    public class MintGarageContext : DbContext
    {
        public MintGarageContext(DbContextOptions<MintGarageContext> option) : base(option)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }

    }
}

