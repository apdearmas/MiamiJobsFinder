using System.Linq;
using BusinessDomain;

namespace BDL
{
    public interface IJobOffersService
    {
        IQueryable<JobOffer> FindAll();
    }
}