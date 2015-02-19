using System.Linq;
using BusinessDomain;
using DAL;

namespace BDL
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public IQueryable<Customer> FindAll()
        {
            return customerRepository.FindAll();
        }
    }
}