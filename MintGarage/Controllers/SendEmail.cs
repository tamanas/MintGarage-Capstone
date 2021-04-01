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
        private string firstName;
        private string lastName;
        private string email;
        private string service;
        private string description;

        public SendEmail(string fName, string lName, string emailAddress, string desc, string serv)
        {
            firstName = fName;
            lastName = lName;
            email = emailAddress;
            description = desc;
            service = serv;
            string messageBody = description;
            messageBody = "Hello Mint Construction Team,\n\n" +
                "This is a consultation request from:\nName: " + firstName + " " + lastName +
                "\nEmail: " + email + 
                "\nService: " + service + 
                "\n\nDO NOT REPLY: Sent as automated email";
            MailAddress to = new MailAddress("anon.maltese@gmail.com");
            MailAddress from = new MailAddress("anon.maltese@gmail.com");
            MailMessage mail = new MailMessage();
            mail.From = from;
            mail.To.Add(to);
            mail.Subject = "Do Not Reply - Mint Construction Consultation Request: " + service;
            mail.Body = messageBody;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(from.Address, "an0n1234");

            //mail.IsBodyHtml = true;
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
