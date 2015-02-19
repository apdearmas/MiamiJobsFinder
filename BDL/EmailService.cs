using System.Net.Mail;
using System.Net;

namespace BDL
{
    public class EmailService : IEmailService
    {
        public ISmtpClientWrapper smtpClientWrapper;

        public EmailService(ISmtpClientWrapper smtpClientWrapper)
        {
            this.smtpClientWrapper = smtpClientWrapper;
        }
                

        public void Send(string targetEmail, string subject, string message)
        {
            smtpClientWrapper.Send(targetEmail, subject, message);
        }
        
    }
}
