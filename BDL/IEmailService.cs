using System.Collections.Generic;
namespace BDL
{
    public interface IEmailService
    {
        void Send(List<string> emailRecipientList, string subject, string message);
    }
}
