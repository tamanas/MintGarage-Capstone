using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MintGarage.Models.ConsultationForms;
using MintGarage.Controllers;
using MintGarage.Database;
using Xunit;
using Moq;

namespace ConsultationFormTests
{
    public class ConsultationFormTest
    {

        Consultation form1 = new Consultation {
            ConsultationFormID = 1000,
            FirstName = "John", 
            LastName = "Smith", 
            EmailAddress = "John.Smith777@gmail.com", 
            PhoneNumber = "000333444", 
            ServiceType = "Build pool", 
            FormDescription = "Need a 10m by 20m pool in backyard"
        };

/*        [Fact]
        public async Task IndexTest()
        {
            // Arrange
            var mockSet = new Mock<DbSet<ConsultationForm>>();
            var mockContext = new Mock<MintGarageContext>();
            mockContext.Setup(ctx => ctx.ConsultationForm).Returns(mockSet.Object);
            var controller = new ConsultationFormsController(mockContext.Object);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.Equal("Index", result.ViewName);
        }*/

        [Fact]
        public void ConsultationIdTest()
        {
            var consultationForm = new Consultation();
            consultationForm.ConsultationFormID = 100;

            Assert.Equal(100, consultationForm.ConsultationFormID);
        }

        [Fact]
        public void FirstNameTest()
        {
            var consultationForm = new Consultation();
            consultationForm.FirstName = "Jane";

            Assert.Equal("Jane", consultationForm.FirstName);
        }

        [Fact]
        public void LastNameTest()
        {
            var consultationForm = new Consultation();
            consultationForm.LastName = "Doe";

            Assert.Equal("Doe", consultationForm.LastName);
        }

        [Fact]
        public void EmailAddressTest()
        {
            var consultationForm = new Consultation();
            consultationForm.EmailAddress = "Jane.Doe@domain.ca";

            Assert.Equal("Jane.Doe@domain.ca", consultationForm.EmailAddress);
        }

        [Fact]
        public void PhoneNumberTest()
        {
            var consultationForm = new Consultation();
            consultationForm.PhoneNumber = "1234567890";

            Assert.Equal("1234567890", consultationForm.PhoneNumber);
        }

        [Fact]
        public void ServiceTypeTest()
        {
            var consultationForm = new Consultation();
            consultationForm.ServiceType = "Build pool";

            Assert.Equal("Build pool", consultationForm.ServiceType);
        }

        [Fact]
        public void FormDescriptionTest()
        {
            var consultationForm = new Consultation();
            consultationForm.FormDescription = "Need a 10m by 20m pool in backyard";

            Assert.Equal("Need a 10m by 20m pool in backyard", consultationForm.FormDescription);
        }
    }
}
