using Microsoft.EntityFrameworkCore;
using MintGarage.Models.AccountT;
using MintGarage.Models.ConsultationT;
using MintGarage.Models.PartnerT;
using MintGarage.Models.HomeTab.Contacts;
using MintGarage.Models.HomeTab.HomeContents;
using MintGarage.Models.HomeTab.Reviews;
using MintGarage.Models.HomeTab.SocialMedias;
using MintGarage.Models.HomeTab.Suppliers;
using MintGarage.Models.FooterContents.FooterContactInfo;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using MintGarage.Models.AboutUsT.TeamMembers;
using MintGarage.Models.AboutUsT.Values;
using MintGarage.Models.GalleryTab;

namespace MintGarage.Database
{
    public class MintGarageContext : DbContext
    {
        public MintGarageContext(DbContextOptions<MintGarageContext> option) : base(option)
        {
        }
        public DbSet<Consultation> Consultation { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<HomeContent> HomeContents { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<FooterContactInfo> FooterContactInfo { get; set; }
        public DbSet<FooterSocialMedia> FooterSocialMedias { get; set; }
        public DbSet<TeamMember> TeamMember { get; set; }
        public DbSet<Value> Value { get; set; }
        public DbSet<Gallery> Gallery { get; set; }

    }
}

