using System.Net;
using System.Net.Mail;
using SendGrid;
using System.Collections.Generic;

namespace BDL
{
    public class SmtpClientWrapper : ISmtpClientWrapper
    {
        public void Send(string targetEmail, string subject, string message)
        {
            IAzureKeyVaultService azureKeyVaultService = AzureKeyVaultService.Instance;

            var credentials = new NetworkCredential(azureKeyVaultService.SendGridUserName, azureKeyVaultService.SendGridPassword);

            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage();
            myMessage.AddTo(targetEmail);
            myMessage.From = new MailAddress("apdearmas@sendgrid.com", "Miami Jobs Finder");
            myMessage.Subject = subject;
            myMessage.Html = message;

            
            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            transportWeb.Deliver(myMessage);
            
        }

        public void Send(List<string> targetEmail, string subject, string message)
        {
            IAzureKeyVaultService azureKeyVaultService = AzureKeyVaultService.Instance;

            var credentials = new NetworkCredential(azureKeyVaultService.SendGridUserName, azureKeyVaultService.SendGridPassword);

            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage();
            myMessage.AddTo(targetEmail);
            myMessage.From = new MailAddress("apdearmas@sendgrid.com", "Miami Jobs Finder");
            myMessage.Subject = subject;
            myMessage.Html = message;


            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            transportWeb.Deliver(myMessage);

        }

    }
}
