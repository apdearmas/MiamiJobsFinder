using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using SendGrid;

namespace BDL
{
    public class SmtpClientWrapper : ISmtpClientWrapper
    {
        public void Send(string targetEmail, string subject, string message)
        {
            var username = ConfigurationManager.AppSettings["SendGridUserName"];
            var pswd = ConfigurationManager.AppSettings["SendGridPassword"];

            var credentials = new NetworkCredential(username, pswd);

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
