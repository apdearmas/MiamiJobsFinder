using System.Collections.Generic;
using System.Linq;
using BusinessDomain;
using System;
using Microsoft.Practices.Unity;

namespace BDL
{
    
    public class SendJobOffersService : ISendJobOffersService
    {
        private readonly ICustomerService customerService;
        private readonly IJobOffersService jobOffersService;
        private readonly IEmailService emailService;
        private IAzureStorageService azureStorageService;

        private const string Subject = "Ofertas de Trabajo";
        
        public SendJobOffersService(IUnityContainer unityContainer, ICustomerService customerService, IJobOffersService jobOffersService, IEmailService emailService)
        {
            this.customerService = customerService;
            this.jobOffersService = jobOffersService;
            this.emailService = emailService;


            azureStorageService = unityContainer.Resolve<IAzureStorageService>();
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

        public string CreateEmailMessage(IEnumerable<JobOffer> jobOffers)
        {
            string emailMessage = String.Empty;
            if (jobOffers != null)
            {
                emailMessage = jobOffers.Aggregate(string.Empty, (current, jobOffer) => 
                    current + 
                    jobOffer.Title + "<br />" +
                    jobOffer.Description + "<br />" +
                    CreateLink(jobOffer.JobOfferFileName) + "<br />" + "<hr>" +
                    "</p>");
            }
            return emailMessage;
        }

        #region Private Methods
        private Uri GetUrlContainer()
        {
            
            return azureStorageService.getAzureContainerUri();
        }

        private string CreateLink(string fileName)
        {
            if (fileName == null) return string.Empty;
            var url = GetUrlContainer().AbsoluteUri  + fileName;
            return string.Format("<p><a href=\"{0}\">{1}</a></p>", url, url);
        }
        #endregion


    }
}