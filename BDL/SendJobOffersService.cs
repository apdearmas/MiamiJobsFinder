using System.Linq;

namespace BDL
{
    
    public class SendJobOffersService : ISendJobOffersService
    {
        private ICustomerService customerService;
        private IJobOffersService jobOffersService;
        private IEmailService emailService;

        private string subject = "Ofertas de Trabajo";
        public string Message = string.Empty;
       
      

        public SendJobOffersService(ICustomerService customerService, IJobOffersService jobOffersService, IEmailService emailService)
        {
            this.customerService = customerService;
            this.jobOffersService = jobOffersService;
            this.emailService = emailService;
        }

        public void SendJobOffers()
        {
            var customers = customerService.FindAll();
            var jobOffers = jobOffersService.FindAll();

            CreateEmailMessage(jobOffers);

            foreach (var customer in customers)
            {
                emailService.Send(customer.EMail, subject, this.Message);
            }
        }

        private string CreateEmailMessage(IQueryable<BusinessDomain.JobOffer> jobOffers)
        {
            if (jobOffers == null) return string.Empty;

            foreach (var jobOffer in jobOffers)
            {
                Message = Message + jobOffer.Description + System.Environment.NewLine; 
            }
            return this.Message;
        }

        
    }
}