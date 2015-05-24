using System.Collections.Generic;
namespace BDL
{
    public class EmailService : IEmailService
    {
        public ISmtpClientWrapper SmtpClientWrapper;

        public EmailService(ISmtpClientWrapper smtpClientWrapper)
        {
            SmtpClientWrapper = smtpClientWrapper;
        }
                

        public void Send(string targetEmail, string subject, string message)
        {
            SmtpClientWrapper.Send(targetEmail, subject, message);
        }

        public void Send(List<string> targetEmail, string subject, string message)
        {
            SmtpClientWrapper.Send(targetEmail, subject, message);
        }

        
    }
}
