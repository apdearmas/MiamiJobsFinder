using System.Net;
using System.Net.Mail;
using SendGrid;

namespace BDL
{
    public class SmtpClientWrapper : ISmtpClientWrapper
    {
        private SmtpClient smtpClient;
        private MailMessage mail;

        const int SmtpPort = 587;
        const string Host = "plus.smtp.mail.yahoo.com";
        const string MailFrom = "elukse@yahoo.com";
        const string Password = "TeamoJesus44";

//        public SmtpClientWrapper()
//        {
//            smtpClient = new SmtpClient(Host)
//            {
//                Port = SmtpPort,
//                UseDefaultCredentials = false,
//                Credentials = new NetworkCredential(MailFrom, Password),
//                DeliveryMethod = SmtpDeliveryMethod.Network,
//                EnableSsl = true
//            };
//        }

        public void Send(string targetEmail, string subject, string message)
        {
//            mail = new MailMessage
//            {
//                From = new MailAddress(MailFrom),
//                Subject = subject,
//                Body = message,
//            };
//            mail.To.Add(targetEmail);
//
//            smtpClient.Send(mail);

            // Create network credentials to access your SendGrid account.
            var username = "apdearmas";
            var pswd = "dollar1Lexus";

            var credentials = new NetworkCredential(username, pswd);


            // Create the email object first, then add the properties.
            SendGridMessage myMessage = new SendGridMessage();
            myMessage.AddTo("apdearmas@yahoo.com");
            myMessage.From = new MailAddress("apdearmas@sendgrid.com", "Antonio Pereira");
            myMessage.Subject = "Testing the SendGrid Library";
            myMessage.Text = "Hello World!";

            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            transportWeb.Deliver(myMessage);
   
        }
    }
}

