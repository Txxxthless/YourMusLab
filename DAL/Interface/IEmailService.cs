namespace DAL.Interface
{
    public interface IEmailService
    {
        void SendEmail(string email, string subject, string message);
    }
}