using System.Collections.Generic;
using System.Linq;
using BusinessDomain;
using Moq;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BDL.Tests
{
    public class SendJobOffersServiceTests
    {
        private readonly SendJobOffersService sendJobOffersService;
        private readonly Mock<ICustomerService> customerServiceMock;
        private readonly Mock<IJobOffersService> jobOffersServiceMock;

        private readonly Mock<IEmailService> emailServiceMock;

        public SendJobOffersServiceTests()
        {
            customerServiceMock = new Mock<ICustomerService>();
            jobOffersServiceMock = new Mock<IJobOffersService>();
            emailServiceMock = new Mock<IEmailService>();

            sendJobOffersService = new SendJobOffersService(customerServiceMock.Object, jobOffersServiceMock.Object, emailServiceMock.Object);
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
            var customer1 = new Customer();
            var customer2 = new Customer();
            var customer3 = new Customer();
            var customerList = new List<Customer> { customer1, customer2, customer3}.AsQueryable();

            customerServiceMock.Setup(m => m.FindAll()).Returns(customerList);
            
            emailServiceMock.Setup(
                m => m.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
 
            sendJobOffersService.SendJobOffers();

            emailServiceMock.Verify(
                m => m.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), 
                Times.Exactly(3));
        }

        [Fact]
        public void VerifyCustomerEmailIsPassedToEmailServiceSend()
        {
            var customer = new Customer { EMail="xxx@x.com" };

            var customerList = new List<Customer> { customer }.AsQueryable();
            
            customerServiceMock.Setup(m => m.FindAll()).Returns(customerList);

            string email = string.Empty;
            
            emailServiceMock.Setup(
                m => m.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string, string>( (s1, s2, s3) => email = s1 );

            sendJobOffersService.SendJobOffers();

            Assert.IsTrue(email.Equals(customer.EMail));

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
                m => m.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string, string>( (s1, s2, s3) => subject = s2 );

            //Act
            sendJobOffersService.SendJobOffers();
            
            //Assert
            Assert.IsTrue(subject.Equals("Ofertas de Trabajo" ));
           

        }

        [Fact]
        public void VerifyMessageIsPassedToEmailServiceSend()
        {
            //Arrange
            var customer = new Customer();
            var customerList = new List<Customer> { customer }.AsQueryable();
            customerServiceMock.Setup(m => m.FindAll()).Returns(customerList);

            JobOffer jobOffer1 = new JobOffer { Id = 1, Description = "Job Offer 1" };
            JobOffer jobOffer2 = new JobOffer { Id = 2, Description = "Job Offer 2" };
            JobOffer jobOffer3 = new JobOffer { Id = 3, Description = "Job Offer 3" };

            string emailMessage = GetTestMessage( jobOffer1,  jobOffer2,  jobOffer3);
            var jobOfferList = new List<JobOffer> { jobOffer1, jobOffer2, jobOffer3 }.AsQueryable();
            jobOffersServiceMock.Setup(m => m.FindAll()).Returns(jobOfferList);
            
            string msg = string.Empty;
            emailServiceMock.Setup(
                m => m.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string, string>((s1, s2, s3) => msg = s3);

            //Act
            sendJobOffersService.SendJobOffers();
            
            //Assert
            Assert.IsTrue(msg.Equals(emailMessage));
           
        }

        [Fact]
        public void VerifyEmailMessageConstruction()
        {
            //Arrange
            JobOffer jobOffer1 = new JobOffer { Id = 1, Description = "Job Offer 1" };
            JobOffer jobOffer2 = new JobOffer { Id = 2, Description = "Job Offer 2" };
            JobOffer jobOffer3 = new JobOffer { Id = 3, Description = "Job Offer 3" };

            string message= GetTestMessage( jobOffer1,  jobOffer2,  jobOffer3);

            var jobOfferList = new List<JobOffer> { jobOffer1, jobOffer2, jobOffer3 }.AsQueryable();

            jobOffersServiceMock.Setup(m => m.FindAll()).Returns(jobOfferList);

            //Act
            sendJobOffersService.SendJobOffers();

            //Assert
            Assert.IsTrue(message.Equals(sendJobOffersService.Message));
        }

        private string GetTestMessage( JobOffer jobOffer1,  JobOffer jobOffer2,  JobOffer jobOffer3)
        {
            return  jobOffer1.Description + System.Environment.NewLine +
                    jobOffer2.Description + System.Environment.NewLine +
                    jobOffer3.Description + System.Environment.NewLine;
        }

    }
}
