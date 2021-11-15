using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MintGarage.Database;
using MintGarage.Models.Categories;
using MintGarage.Models.Products;
using MintGarage.Models.AccountT;
using MintGarage.Models.FooterContents.FooterContactInfo;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models
{
    public class InitialData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            MintGarageContext context = app.ApplicationServices
                                        .CreateScope().ServiceProvider
                                        .GetRequiredService<MintGarageContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Category.Any())
            {
                context.Category.Add(new Category() { Name = "Hook" });
                context.Category.Add(new Category() { Name = "Shelf" });
                context.Category.Add(new Category() { Name = "Basket" });
                context.SaveChanges();
            }

            if (!context.Account.Any())
            {
                context.Account.Add(new Account() { Username = "admin", Password = "admin" });
                context.SaveChanges();
            }
            if (!context.FooterContactInfo.Any())
            {
                context.FooterContactInfo.Add(new FooterContactInfo()
                {
                    PhoneNumber = "6475230914",
                    EmailAddress = "Info@mintgarage.ca"
                });
                context.SaveChanges();
            }
            if (!context.Value.Any())
            {
                context.Value.Add(new AboutUsTab.Values.Value()
                {
                    ValueTitle = "Great Services",
                    ValueDescription = "With our range or services from Garage Organization solutions to, " +
                    "epoxy floors, to our other services we do it all!",
                    ValueImage = "homeValueIcon.png"
                });
                context.Value.Add(new AboutUsTab.Values.Value()
                {
                    ValueTitle = "Highest Standards",
                    ValueDescription = "Every site is managed by a supervisor and daily reports / " +
                    "inspections are done to ensure our quality guarantee!",
                    ValueImage = "gearValueIcon.png"
                });
                context.Value.Add(new AboutUsTab.Values.Value()
                {
                    ValueTitle = "Professional Team",
                    ValueDescription = "We train our employees using our state of the art " +
                    "online platform so every job is a consistent one!",
                    ValueImage = "peopleValueIcon.png"
                });
                context.Value.Add(new AboutUsTab.Values.Value()
                {
                    ValueTitle = "Creative Solutions",
                    ValueDescription = "With every customer is a unique solution, thats why in " +
                    "phase one we show you a visual solution before we do it!",
                    ValueImage = "lightValueIcon.jpg"
                });
                context.SaveChanges();
            }
            if (!context.Product.Any())
            {
                context.Product.Add(new Product()
                {
                    ProductName = "Shelf",
                    ProductPrice = 4.99,
                    ProductImage = "Shelf.png",
                    CategoryID = 2
                });
                context.Product.Add(new Product()
                {
                    ProductName = "Recycle Bin Hook",
                    ProductPrice = 5.99,
                    ProductImage = "RecycleBinHook.png",
                    CategoryID = 1
                });
                context.Product.Add(new Product()
                {
                    ProductName = "Big Rectangle Basket",
                    ProductPrice = 10.99,
                    ProductImage = "BigRectangleBasket.png",
                    CategoryID = 3
                });
                context.SaveChanges();
            }
        }

    }
}
