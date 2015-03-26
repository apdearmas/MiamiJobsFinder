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
            
        }
    }
}

