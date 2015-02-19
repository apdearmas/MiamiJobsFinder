using System.Linq;
using BusinessDomain;

namespace BDL
{
    public interface ICustomerService
    {
        IQueryable<Customer> FindAll();
    }
}

