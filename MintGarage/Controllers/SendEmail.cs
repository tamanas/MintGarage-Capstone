using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace MintGarage.Controllers
{
    public class SendEmail
    {

        private string email;
        private string service;
        private string description;

        public SendEmail(string e, string d, string s)
        {
            email = e;
            service = s;
            description = d;
            string messageBody = description;
            MailAddress from = new MailAddress("anonymous.testicle@gmail.com");
            MailAddress to = new MailAddress(email);
            MailMessage mail = new MailMessage();
            mail.From = from;
            mail.To.Add(to);
            mail.Subject = "Mint Construction Consultation Request: " + service;
            mail.Body = messageBody;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(from.Address, "an0n1234");

            mail.IsBodyHtml = true;
            try
            {
                client.Send(mail);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
