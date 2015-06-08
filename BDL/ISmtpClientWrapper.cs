using System.Collections.Generic;

namespace BDL
{
    public interface  ISmtpClientWrapper
    {
        void Send(List<string> targetEmail, string subject, string message);
    }
}