using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.ConsultationForms
{
    public interface IConsultationFormRepository
    {
        IQueryable<ConsultationForm> ConsultationForms { get; }
    }
}

