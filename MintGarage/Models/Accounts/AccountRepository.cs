using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private MintGarageContext context;
        public AccountRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Account> Account => context.Account;

        public void Update(Account account)
        {
            context.Account.Update(account);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
