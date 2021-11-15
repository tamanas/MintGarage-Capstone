using Microsoft.EntityFrameworkCore;
using MintGarage.Models.AccountT;
using MintGarage.Models.ConsultationT;
using MintGarage.Models.PartnerT;
using MintGarage.Models.HomeT.Cards;
using MintGarage.Models.HomeT.Reviews;
using MintGarage.Models.HomeT.Suppliers;
using MintGarage.Models.FooterT.ContactInformation;
using MintGarage.Models.FooterT.SocialMedias;
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
        public DbSet<Card> Card { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<SocialMedia> SocialMedia { get; set; }
        public DbSet<TeamMember> TeamMember { get; set; }
        public DbSet<Value> Value { get; set; }
        public DbSet<Gallery> Gallery { get; set; }

    }
}

