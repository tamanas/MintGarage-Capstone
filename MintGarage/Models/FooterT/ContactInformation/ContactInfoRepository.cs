using System.Linq;
using MintGarage.Database;

namespace MintGarage.Models.FooterT.ContactInformation
{
    public class ContactInfoRepository : IRepository<ContactInfo>
    {
        private MintGarageContext context;
        public ContactInfoRepository(MintGarageContext ctx)
        {
            context = ctx;
        }

        public IQueryable<ContactInfo> Items => context.ContactInfo;

        public void Create(ContactInfo item)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ContactInfo item)
        {
                context.ContactInfo.Update(item);
                Save();
        }

        public void Delete(ContactInfo item)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
