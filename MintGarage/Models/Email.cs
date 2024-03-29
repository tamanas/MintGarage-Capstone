﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using MintGarage.Models.ConsultationT;

namespace MintGarage.Controllers
{
    public class Email
    {
        private Consultation consultation;

        public Email(Consultation c)
        {
            consultation = c;
        }

        public void SendEmail()
        {
            string firstName = consultation.FirstName;
            string lastName = consultation.LastName;
            string email = consultation.EmailAddress;
            string phoneNumber = consultation.PhoneNumber;
            string description = consultation.FormDescription;
            string service = consultation.ServiceType;
            string messageBody = consultation.FormDescription;
            messageBody = "Hello Mint Garage,\n\n" +
                "This is a consultation request from:\nName: " + firstName + " " + lastName +
                "\nEmail: " + email + 
                "\nPhone #: " + phoneNumber +
                "\nService: " + service + 
                "\n\nDescription:\n" + description +
                "\n\n\nThis is an automated email.";
            MailAddress to = new MailAddress("info.mintgarage@gmail.com");
            MailAddress from = new MailAddress("info.mintgarage@gmail.com");
            MailMessage mail = new MailMessage();
            mail.From = from;
            mail.To.Add(to);
            mail.Subject = "Consultation Request from: " + firstName + " " + lastName;
            mail.Body = messageBody;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(from.Address, "an0n1234");

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
