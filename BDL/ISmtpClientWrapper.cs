using System.Net.Mail;

namespace BDL
{
    public interface  ISmtpClientWrapper
    {
        void Send(string targetEmail, string subject, string message);
    }
}