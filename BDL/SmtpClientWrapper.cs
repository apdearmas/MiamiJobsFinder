using System.Net;
using System.Net.Mail;
using SendGrid;

namespace BDL
{
    public class SmtpClientWrapper : ISmtpClientWrapper
    {
        public void Send(string targetEmail, string subject, string message)
        {
            // Create network credentials to access your SendGrid account.
            const string username = "apdearmas";
            const string pswd = "dollar1Lexus";

            var credentials = new NetworkCredential(username, pswd);


            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage();
            myMessage.AddTo(targetEmail);
            myMessage.From = new MailAddress("apdearmas@sendgrid.com", "Antonio Pereira");
            myMessage.Subject = subject;
            myMessage.Text = message;

            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            transportWeb.Deliver(myMessage);
   
        }
    }
}

