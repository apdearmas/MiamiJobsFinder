using System.Collections.Generic;
using BusinessDomain;
using System;

namespace BDL
{
    
    public class SendJobOffersService : ISendJobOffersService
    {
        private readonly ICustomerService customerService;
        private readonly IJobOffersService jobOffersService;
        private readonly IEmailService emailService;

        private const string Subject = "Ofertas de Trabajo";
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
                emailService.Send(customer.EMail, Subject, Message);
            }
        }

        private void CreateEmailMessage(IEnumerable<JobOffer> jobOffers)
        {
            if (jobOffers == null) return;

            foreach (var jobOffer in jobOffers)
            {
                Message = Message + jobOffer.Description + System.Environment.NewLine + CreateLink(jobOffer.JobOfferFileName) + System.Environment.NewLine; 
            }
        }

        #region Private Methods
        private Uri GetUrlContainer()
        {
            AzureStorageService azureStorageService = new AzureStorageService();
            return azureStorageService.getAzureContainerUri();
        }

        private string CreateLink(string fileName)
        {
            return GetUrlContainer().AbsoluteUri + "/" + fileName;
        }
        #endregion


    }
}