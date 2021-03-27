using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MintGarage.Models.ConsultationForms;

namespace MintGarage.Models.Service
{
    public class TypeService
    {
        [Key]
        public int ServiceID { get; set; }

        public string ServiceName { get; set; }

        [ForeignKey("ServiceID")]
        public virtual ICollection<ConsultationForm> ConsultationForm { get; set; }
    }
}

