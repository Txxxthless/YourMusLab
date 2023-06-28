using System.Net;
using System.Net.Mail;
using DAL.Interface;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace DAL.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string email, string subject, string message)
        {
            using var client = new SmtpClient()
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.office365.com",
                Port = 587,
                Credentials = new NetworkCredential(
                    _configuration["EmailConfig:Email"],
                    _configuration["EmailConfig:Password"]
                ),
            };

            var emailMessage = new MailMessage(
                _configuration["EmailConfig:Email"],
                email,
                subject,
                message
            )
            {
                IsBodyHtml = true
            };

            try
            {
                client.Send(emailMessage);
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.Message;
            }
        }
    }
}
