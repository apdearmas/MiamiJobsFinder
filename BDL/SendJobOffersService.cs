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

            List<string> emailRecipientList = RecipientList(customers);

            //emailService.Send(customer.EMail, Subject, Message);
            emailService.Send(emailRecipientList, Subject, Message);
        }

        private static List<string> RecipientList(System.Linq.IQueryable<Customer> customers)
        {
            List<string> emailList = new List<string>();
            foreach (var customer in customers)
            {
                emailList.Add(customer.EMail);               

            }
            return emailList;
        }

        private void CreateEmailMessage(IEnumerable<JobOffer> jobOffers)
        {
            if (jobOffers == null) return;

            foreach (var jobOffer in jobOffers)
            {
                Message = Message + jobOffer.Title + System.Environment.NewLine +
                                    jobOffer.Description + System.Environment.NewLine +
                                    CreateLink(jobOffer.JobOfferFileName) + System.Environment.NewLine + 
                                    "_____________________________" + System.Environment.NewLine;
            }
        }

        #region Private Methods
        private Uri GetUrlContainer()
        {
            AzureStorageService azureStorageService = AzureStorageService.Instance;
            return azureStorageService.getAzureContainerUri();
        }

        private string CreateLink(string fileName)
        {
            if (fileName == null) return null;
            var url = GetUrlContainer().AbsoluteUri + "/" + fileName;
            return string.Format("<p><a href=\"{0}\">{1}</a></p>", url, url);
        }
        #endregion


    }
}