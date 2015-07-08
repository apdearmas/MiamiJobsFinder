using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace BusinessDomain
{
 
    public class JobOffer
    {
        const int DAYSPAN = 30;

        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Issued Date")]
        public DateTime IssuedDate { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        [AllowHtml]
        [UIHint("tinymce_full")]
        public string Description { get; set; }
        public ContactPerson ContactPerson { get; set; }
        public Location Location { get; set; }

        [Display(Name = "File Name")]
        public string JobOfferFileName { get; set; }

        
        public JobOffer()
        {
            this.IssuedDate = DateTime.Now;
            this.ExpirationDate = IssuedDate.AddDays(DAYSPAN);
        }
    }
}
