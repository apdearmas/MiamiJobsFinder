namespace BDL
{
    public interface IEmailService
    {
        void Send(string targetEmail, string subject, string message);

    }
}
