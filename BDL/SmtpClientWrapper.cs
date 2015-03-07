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

            //            using (var attachmentFileStream = new FileStream(@"C:\dev\Giraffas.pdf", FileMode.Open))
            //            {
            //                myMessage.AddAttachment(attachmentFileStream, "Giraffas.pdf");
            //            }
            //            using (var attachmentFileStream = new FileStream(@"C:\dev\lasership.pdf", FileMode.Open))
            //            {
            //                myMessage.AddAttachment(attachmentFileStream, "lasership.pdf");
            //            }

            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            transportWeb.Deliver(myMessage);

        }
    }
}

//Giraffas