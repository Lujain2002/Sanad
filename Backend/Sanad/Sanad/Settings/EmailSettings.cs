using Microsoft.Extensions.Options;
using Sanad.Models;
using System.Net.Mail;
using System.Net;

namespace Sanad.Settings
{
    public class EmailSettings
    {
        private readonly EmailConfig _config;

        public EmailSettings(IOptions<EmailConfig> config)
        {
            _config = config.Value;
        }

        public void SendEmail(Email email)
        {
            var client = new SmtpClient(_config.SmtpServer, _config.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_config.SenderEmail, _config.SenderPassword)
            };

            client.Send(_config.SenderEmail, email.Recivers, email.Subject, email.Body);
        }
    }
}
