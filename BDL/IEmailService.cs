namespace BDL
{
    public interface IEmailService
    {
        void Send(string targetEmail, string subject, string message);


        void Send(System.Collections.Generic.List<string> emailRecipientList, string Subject, string Message);
    }
}
