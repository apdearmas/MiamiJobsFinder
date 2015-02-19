using System.Collections.Generic;
using System.Linq;
using BusinessDomain;
using DAL;
using Moq;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BDL.Tests
{
    public class CustomerServiceTests
    {
        private readonly CustomerService customerService;
        private readonly Mock<IRepository<Customer>> customerRepositoryMock;

        public CustomerServiceTests()
        {
            customerRepositoryMock = new Mock<IRepository<Customer>>();
            customerService = new CustomerService(customerRepositoryMock.Object);
        }

        [Fact]
        public void Create()
        {
            Assert.IsNotNull(customerService);
        }

        [Fact]
        public void FindAll()
        {

            var customers = new List<Customer>
            {
                new Customer{Id = 1, Name = "c1", EMail = "c1@xyz.com"}, 
                new Customer{Id = 2, Name = "c2", EMail = "c2@xyz.com"}, 
            };
            customerRepositoryMock.Setup(m => m.FindAll()).Returns(customers.AsQueryable());

            var result = customerService.FindAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.First().Name.Equals("c1"));
            Assert.IsTrue(result.Last().EMail.Equals("c2@xyz.com"));
        }
    }

}
