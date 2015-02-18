using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain
{
    public class JobOfferListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Description { get; set; }
        public ContactPerson ContactPerson { get; set; }
        public Location Location { get; set; }
    }
}
