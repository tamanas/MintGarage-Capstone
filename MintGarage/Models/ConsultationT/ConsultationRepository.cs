using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.ConsultationT
{
    public class ConsultationRepository : IRepository<Consultation>
    {
        private MintGarageContext context;
        public ConsultationRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Consultation> Items => context.Consultation;

        public void Create(Consultation item)
        {
            context.Consultation.Add(item);
            Save();
        }

        public void Delete(Consultation item)
        {
            context.Consultation.Remove(item);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Consultation item)
        {
            throw new NotImplementedException();
        }
    }
}