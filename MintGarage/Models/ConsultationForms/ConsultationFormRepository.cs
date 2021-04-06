using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.ConsultationForms
{
    public class ConsultationFormRepository : IConsultationFormRepository
    {
        private MintGarageContext context;
        public ConsultationFormRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<ConsultationForm> ConsultationForms => context.ConsultationForm;

        public void AddConsultationForm(ConsultationForm consultationForm)
        {
            context.ConsultationForm.Add(consultationForm);
            SaveConsultationForm();
        }

        public void SaveConsultationForm()
        {
            context.SaveChanges();
        }
    }
}