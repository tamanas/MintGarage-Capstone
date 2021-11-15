using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MintGarage.Database;
using MintGarage.Models;
using MintGarage.Models.AccountT;
using MintGarage.Models.HomeT.Cards;
using MintGarage.Models.HomeT.Reviews;
using MintGarage.Models.HomeT.Suppliers;
using MintGarage.Models.ConsultationT;
using MintGarage.Models.FooterT.ContactInformation;
using MintGarage.Models.FooterT.SocialMedias;
using MintGarage.Models.PartnerT;
using MintGarage.Models.AboutUsT.TeamMembers;
using MintGarage.Models.AboutUsT.Values;
using MintGarage.Models.GalleryTab;

namespace MintGarage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Adding session services to Razor Pages
            services.AddSession();
            services.AddMemoryCache();
            services.AddMvc();

            services.AddControllersWithViews();
            services.AddDbContext<MintGarageContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:MintGarageConnStr"]);
            });

            services.AddScoped<IRepository<Consultation>, ConsultationRepository>();
            services.AddScoped<IRepository<Account>, AccountRepository>();
            services.AddScoped<IRepository<Card>, CardRepository>();
            services.AddScoped<IRepository<Review>, ReviewRepository>();
            services.AddScoped<IRepository<Supplier>, SupplierRepository>();
            services.AddScoped<IRepository<ContactInfo>, ContactInfoRepository>();
            services.AddScoped<IRepository<SocialMedia>, SocialMediaRepository>();
            services.AddScoped<IRepository<Partner>, PartnerRepository>();
            services.AddScoped<IRepository<TeamMember>, TeamMemberRepository>();
            services.AddScoped<IRepository<Value>, ValueRepository>();
            services.AddScoped<IRepository<Gallery>, GalleryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            /* if (env.IsDevelopment())
             {
                 app.UseDeveloperExceptionPage();
             }
             else
             {
                 app.UseExceptionHandler("/Home/Error");
                 // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                 app.UseHsts();
             }*/
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<MintGarageContext>())
                context.Database.Migrate();
            //  mintGarageDBInitializer.Initialize();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            InitialData.EnsurePopulated(app);
        }
    }
}
