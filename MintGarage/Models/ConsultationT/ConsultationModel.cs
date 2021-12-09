using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.ConsultationT
{
    public class ConsultationModel
    {
        public IEnumerable<Consultation> Consultations { get; set; }
        public Consultation Consultation { get; set; }
        public string ImageFile { get; } = "~/Images/consultation/";
        public string TabImage { get; } = "construction.jpg";
        public string TabImageSlogon { get; } = "MINT GARAGE PUTS THE FOCUS BACK ON EXCEPTIONAL SERVICE";
        public string ConsultTitle { get; } = "Talk to Us - We're Here to Help";
        public string ConsultDesc { get; } = "No job would be too big or too small, so if you would be interested in getting some info, feel free to get in touch with us or pass our info along to anyone you know might benefit from what Mint Garage can offer";

    }
}
