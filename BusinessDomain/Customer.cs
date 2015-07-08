using System.ComponentModel.DataAnnotations;



namespace BusinessDomain
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string EMail { get; set; }
    }
}
