using Microsoft.EntityFrameworkCore;
using MintGarage.Models;
using MintGarage.Models.Accounts;
using MintGarage.Models.Categories;
using MintGarage.Models.ConsultationForms;
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
        public DbSet<ConsultationForm> ConsultationForm { get; set; }
        public DbSet<Account> Account { get; set; }
    }
}

