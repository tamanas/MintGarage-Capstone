using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.HomeTab.Contacts
{
    public interface IContactRepository
    {
        IQueryable<Contact> Contacts { get; }
        void AddContacts(Contact contacts);
        void DeleteContacts(Contact contacts);
        void UpdateContacts(Contact contacts);
        void SaveContacts();
    }
}

