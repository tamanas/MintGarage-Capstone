using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Accounts
{
    public interface IAccountRepository
    {
        IQueryable<Account> Account { get; }

        void Update(Account account);
    }
}
