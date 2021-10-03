using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.HomeTab.Contacts
{
    public class ContactsRepository : IContactRepository
    {
        private MintGarageContext context;
        public ContactsRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Contact> Contacts => context.Contacts;

        public void AddContacts(Contact contacts)
        {
            context.Contacts.Add(contacts);
            SaveContacts();
        }

        public void UpdateContacts(Contact contacts)
        {
            context.Contacts.Update(contacts);
            SaveContacts();
        }

        public void DeleteContacts(Contact contacts)
        {
            context.Contacts.Remove(contacts);
            SaveContacts();
        }
        public void SaveContacts()
        {
            context.SaveChanges();
        }
    }
}