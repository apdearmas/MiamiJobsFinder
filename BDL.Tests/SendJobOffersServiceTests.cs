using System.Collections.Generic;
using System.Linq;
using BusinessDomain;
using Moq;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Microsoft.Practices.Unity;
using System;

namespace BDL.Tests
{
    public class SendJobOffersServiceTests
    {
        private IUnityContainer container;
        private readonly SendJobOffersService sendJobOffersService;
        private readonly Mock<ICustomerService> customerServiceMock;
        private readonly Mock<IJobOffersService> jobOffersServiceMock;
        private readonly Mock<IAzureStorageService> azureStorageServiceMock;

        private readonly Mock<IEmailService> emailServiceMock;

        public SendJobOffersServiceTests()
        {
            customerServiceMock = new Mock<ICustomerService>();
            jobOffersServiceMock = new Mock<IJobOffersService>();
            emailServiceMock = new Mock<IEmailService>();
            azureStorageServiceMock = new Mock<IAzureStorageService>();

            container = new UnityContainer();
            container.RegisterInstance(typeof(IAzureStorageService), azureStorageServiceMock.Object);

            sendJobOffersService = new SendJobOffersService(container, customerServiceMock.Object, jobOffersServiceMock.Object, emailServiceMock.Object);
        }

        [Fact]
        public void Create()
        {
            Assert.IsNotNull(sendJobOffersService);
        }

        [Fact]
        public void CustomerServiceFindAllIsCalled()
        {
            var customerList = new List<Customer>().AsQueryable();
            customerServiceMock.Setup(m => m.FindAll()).Returns(customerList).Verifiable();
            sendJobOffersService.SendJobOffers();
            customerServiceMock.Verify(m => m.FindAll(), Times.Once);
        }

        [Fact]
        public void JobOffersServiceFindAllIsCalled()
        {
            jobOffersServiceMock.Setup(m => m.FindAll()).Verifiable();
            sendJobOffersService.SendJobOffers();
            jobOffersServiceMock.Verify(m => m.FindAll(), Times.Once);
        }

        [Fact]
        public void SendJobOffersToEachCustomer()
        {
            var customer1 = new Customer{EMail="xxx@yahoo.com"};
            var customer2 = new Customer{EMail="xxx@yahoo.com"};
            var customer3 = new Customer { EMail = "xxx@yahoo.com" };
            var customerList = new List<Customer> { customer1, customer2, customer3}.AsQueryable();

            customerServiceMock.Setup(m => m.FindAll()).Returns(customerList);

            emailServiceMock.Setup(
                m => m.Send(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
 
            sendJobOffersService.SendJobOffers();

            emailServiceMock.Verify(
                m => m.Send(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()), 
                Times.Exactly(1));
            
        }

        [Fact]
        public void VerifyCustomerEmailIsPassedToEmailServiceSend()
        {
            var customer = new Customer { EMail="xxx@x.com" };

            var customerList = new List<Customer> { customer }.AsQueryable();
            
            customerServiceMock.Setup(m => m.FindAll()).Returns(customerList);

            List<string> email= new List<string>();
            
            emailServiceMock.Setup(
                m => m.Send(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()))
                .Callback<List<string>, string, string>( (s1, s2, s3) => email = s1 );

            sendJobOffersService.SendJobOffers();

            Assert.IsTrue(email.Contains(customer.EMail));

        }

        [Fact]
        public void VerifySubjectIsPassedToEmailServiceSend()
        {
            //Arrange
            var customer = new Customer();
            var customerList = new List<Customer> { customer }.AsQueryable();
            customerServiceMock.Setup(m => m.FindAll()).Returns(customerList);

            
            string subject = string.Empty;

            emailServiceMock.Setup(
                m => m.Send(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()))
                .Callback<List<string>, string, string>( (s1, s2, s3) => subject = s2 );

            //Act
            sendJobOffersService.SendJobOffers();
            
            //Assert
            Assert.IsTrue(subject.Equals("Ofertas de Trabajo" ));
           

        }

        [Fact]
        public void VerifyMessageIsPassedToEmailServiceSend()
        {
            //Arrange
            var customer = new Customer { EMail="xxx@yahoo.com"};
            var customerList = new List<Customer> { customer }.AsQueryable();
            customerServiceMock.Setup(m => m.FindAll()).Returns(customerList);

            

            JobOffer jobOffer1 = new JobOffer { Id = 1, Description = "Job Offer 1" };
            JobOffer jobOffer2 = new JobOffer { Id = 2, Description = "Job Offer 2" };
            JobOffer jobOffer3 = new JobOffer { Id = 3, Description = "Job Offer 3" };

            string emailMessage = GetTestMessage( jobOffer1,  jobOffer2,  jobOffer3);
            List<string> emailList = new List<string>();

            var jobOfferList = new List<JobOffer> { jobOffer1, jobOffer2, jobOffer3 }.AsQueryable();
            jobOffersServiceMock.Setup(m => m.FindAll()).Returns(jobOfferList);
            
            string msg = string.Empty;
            emailServiceMock.Setup(
                 m => m.Send(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()))
                 .Callback<List<string>, string, string>((emailRecipientList, subject, message) => msg = message);

            //Act
            sendJobOffersService.SendJobOffers();
            
            //Assert
            Assert.IsTrue(msg.Equals(emailMessage));
           
        }

        [Fact]
        public void VerifyEmailMessageConstruction()
        {
            //Arrange
            JobOffer jobOffer1 = new JobOffer { Id = 1, Title = "jobOffer1", Description = "Job Offer 1",JobOfferFileName ="test1.net" };
            JobOffer jobOffer2 = new JobOffer { Id = 2, Title = "jobOffer2", Description = "Job Offer 2", JobOfferFileName = "test2.net" };
            JobOffer jobOffer3 = new JobOffer { Id = 3, Title = "jobOffer3", Description = "Job Offer 3", JobOfferFileName = "test3.net" };

            string message= GetTestMessage( jobOffer1,  jobOffer2,  jobOffer3);

            var jobOfferList = new List<JobOffer> { jobOffer1, jobOffer2, jobOffer3 }.AsQueryable();

            Uri uri = new Uri("http://www.google.com");
            azureStorageServiceMock.Setup(m => m.getAzureContainerUri()).Returns(uri);

            //Act
            string emailMessage = sendJobOffersService.CreateEmailMessage(jobOfferList);

            //Assert
            Assert.IsTrue(message.Equals(emailMessage));
        }

        private string GetTestMessage( JobOffer jobOffer1,  JobOffer jobOffer2,  JobOffer jobOffer3)
        {
            string s1 = string.Empty;
            string s2 = string.Empty;
            string s3 = string.Empty;

            if(jobOffer1.JobOfferFileName != null)
            {
                s1 = string.Format("<p><a href=\"{0}\">{1}</a></p>", "http://www.google.com" + "/" + jobOffer1.JobOfferFileName, "http://www.google.com" + "/" + jobOffer1.JobOfferFileName);
            }
            if (jobOffer2.JobOfferFileName != null)
            {
                s2 = string.Format("<p><a href=\"{0}\">{1}</a></p>", "http://www.google.com" +"/" + jobOffer2.JobOfferFileName, "http://www.google.com"+"/" + jobOffer2.JobOfferFileName);
            }
            if (jobOffer3.JobOfferFileName != null)
            {
                s3 = string.Format("<p><a href=\"{0}\">{1}</a></p>", "http://www.google.com" + "/" + jobOffer3.JobOfferFileName, "http://www.google.com" + "/" + jobOffer3.JobOfferFileName);
            }
             return jobOffer1.Title + "<br />" +
                    jobOffer1.Description + "<br />" +
                    s1 +
                     "<br />" + "<hr>" + "</p>" +

                    jobOffer2.Title + "<br />" +
                    jobOffer2.Description + "<br />" +
                    s2 +
                     "<br />" + "<hr>" + "</p>" +
                    
                    jobOffer3.Title + "<br />" +
                    jobOffer3.Description + "<br />" +
                    s3 +
                    "<br />" + "<hr>" + "</p>";
        }

    }
}
