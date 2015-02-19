using System;
using System.Collections.Generic;
using System.Linq;
using BusinessDomain;
using DAL;
using Moq;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BDL.Tests
{
    public class JobOffersServiceTests
    {
        private readonly IJobOffersService jobOffersService;
        private readonly Mock<IRepository<JobOffer>> jobOfferRepositoryMock;

        public JobOffersServiceTests()
        {
            jobOfferRepositoryMock = new Mock<IRepository<JobOffer>>();
            jobOffersService = new JobOffersService(jobOfferRepositoryMock.Object);
        }

        [Fact]
        public void Create()
        {
            Assert.IsNotNull(jobOffersService);
        }

        [Fact]
        public void FindAll()
        {
            var contactPerson = new ContactPerson{Id = 1, Name = "cp", PhoneNumber = "", EMail = ""};
            var jobOffers = new List<JobOffer>
            {
                new JobOffer{Id = 1, ContactPerson = contactPerson, Description = "job1", ExpirationDate = new DateTime(), IssuedDate = new DateTime()}, 
                new JobOffer{Id = 2, ContactPerson = contactPerson, Description = "job2", ExpirationDate = new DateTime(), IssuedDate = new DateTime()}, 
            };
            jobOfferRepositoryMock.Setup(m => m.FindAll()).Returns(jobOffers.AsQueryable());

            var result = jobOffersService.FindAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.First().Description.Equals("job1"));
            Assert.IsTrue(result.Last().Description.Equals("job2"));
        }

    }
}