using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace BusinessDomain
{
 
    public class JobOffer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        public string Description { get; set; }
        public ContactPerson ContactPerson { get; set; }
        public Location Location { get; set; }

        
        
    }
}
