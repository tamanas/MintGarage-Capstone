using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MintGarage.Database;
using MintGarage.Models.AccountT;
using MintGarage.Models.FooterT.ContactInformation;
using System.Linq;
using MintGarage.Models.AboutUsT.Values;

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
          

            if (!context.Account.Any())
            {
                context.Account.Add(new Account() { Username = "admin", Password = "admin" });
                context.SaveChanges();
            }
            if (!context.ContactInfo.Any())
            {
                context.ContactInfo.Add(new ContactInfo()
                {
                    PhoneNumber = "6475230914",
                    EmailAddress = "Info@mintgarage.ca"
                });
                context.SaveChanges();
            }
            if (!context.Value.Any())
            {
                context.Value.Add(new Value()
                {
                    ValueTitle = "Great Services",
                    ValueDescription = "With our range or services from Garage Organization solutions to, " +
                    "epoxy floors, to our other services we do it all!",
                    ValueImage = "homeValueIcon.png"
                });
                context.Value.Add(new Value()
                {
                    ValueTitle = "Highest Standards",
                    ValueDescription = "Every site is managed by a supervisor and daily reports / " +
                    "inspections are done to ensure our quality guarantee!",
                    ValueImage = "gearValueIcon.png"
                });
                context.Value.Add(new Value()
                {
                    ValueTitle = "Professional Team",
                    ValueDescription = "We train our employees using our state of the art " +
                    "online platform so every job is a consistent one!",
                    ValueImage = "peopleValueIcon.png"
                });
                context.Value.Add(new Value()
                {
                    ValueTitle = "Creative Solutions",
                    ValueDescription = "With every customer is a unique solution, thats why in " +
                    "phase one we show you a visual solution before we do it!",
                    ValueImage = "lightValueIcon.jpg"
                });
                context.SaveChanges();
            }
      
        }

    }
}
