using System.Collections.Generic;
using System.Linq;
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

            var message = CreateEmailMessage(jobOffers);

            List<string> emailRecipientList = CreateRecipientList(customers);

            emailService.Send(emailRecipientList, Subject, message);
        }

        private static List<string> CreateRecipientList(IEnumerable<Customer> customers)
        {
            return customers.Select(customer => customer.EMail).ToList();
        }

        private string CreateEmailMessage(IEnumerable<JobOffer> jobOffers)
        {
            if (jobOffers != null)
            {
                return jobOffers.Aggregate(string.Empty, (current, jobOffer) => 
                    current + 
                    jobOffer.Title + "<br />" +
                    jobOffer.Description + "<br />" +
                    CreateLink(jobOffer.JobOfferFileName) + "<br />" + "<hr>" +
                    "</p>");
            }
            return string.Empty;
        }

        #region Private Methods
        private Uri GetUrlContainer()
        {
            AzureStorageService azureStorageService = AzureStorageService.Instance;
            return azureStorageService.getAzureContainerUri();
        }

        private string CreateLink(string fileName)
        {
            if (fileName == null) return string.Empty;
            var url = GetUrlContainer().AbsoluteUri + "/" + fileName;
            return string.Format("<p><a href=\"{0}\">{1}</a></p>", url, url);
        }
        #endregion


    }
}