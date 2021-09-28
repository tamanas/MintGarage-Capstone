using Microsoft.EntityFrameworkCore;
using MintGarage.Models;
using MintGarage.Models.Accounts;
using MintGarage.Models.Categories;
using MintGarage.Models.ConsultationForms;
using MintGarage.Models.HomeTab.Contacts;
using MintGarage.Models.HomeTab.HomeContents;
using MintGarage.Models.HomeTab.Reviews;
using MintGarage.Models.HomeTab.SocialMedias;
using MintGarage.Models.HomeTab.Suppliers;
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
        public DbSet<HomeContent> HomeContents { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Contact> Contacts { get; set; }


    }
}

