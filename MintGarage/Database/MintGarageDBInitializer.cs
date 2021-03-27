using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Database
{
    public class MintGarageDBInitializer : IMintGarageDBInitializer
    {
        /*public MintGarageContext context;

        public MintGarageDBInitializer(MintGarageContext ctx)
        {
            context = ctx;
        }*/
        public void Initialize()
        {
        /*    if (context.Database.GetPendingMigrations().Count() > 0)
            {
                context.Database.Migrate();
            }*/
        }
    }
}
