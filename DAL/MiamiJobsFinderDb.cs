using System.Data.Entity;
using System.Diagnostics;
using BusinessDomain;

namespace DAL
{
    public class MiamiJobsFinderDb : DbContext
    {

        public MiamiJobsFinderDb()
            : base("JobFinderContext")
        {
        }

        public static MiamiJobsFinderDb Create()
        {
            return new MiamiJobsFinderDb();
        }

        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<Location> Locations { get; set; }

    //    public System.Data.Entity.DbSet<BusinessDomain.JobOfferListViewModel> JobOfferListViewModels { get; set; }

    //    public System.Data.Entity.DbSet<BusinessDomain.JobOfferListViewModel> JobOfferListViewModels { get; set; }

    }
}
