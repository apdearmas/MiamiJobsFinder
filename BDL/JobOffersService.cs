using System.Linq;
using BusinessDomain;
using DAL;

namespace BDL
{
    public class JobOffersService : IJobOffersService
    {
        private IRepository<JobOffer> jobOffersRepository;

        public JobOffersService(IRepository<JobOffer> jobOffersRepository)
        {
            this.jobOffersRepository = jobOffersRepository;
        }

        public IQueryable<JobOffer> FindAll()
        {
            return jobOffersRepository.FindAll();
        }
    }
}