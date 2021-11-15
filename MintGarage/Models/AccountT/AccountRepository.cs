using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.AccountT
{
    public class AccountRepository : IRepository<Account>
    {
        private MintGarageContext context;
        public AccountRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Account> Items => context.Account;

        public void Update(Account item)
        {
            context.Account.Update(item);
            Save();
        }
        public void Create(Account item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Account item)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
