using AspNetCoreIdentityApp.Web.OptionsModel;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace AspNetCoreIdentityApp.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendResetEmail(string resetmailLink, string ToEmail)
        {
            var smptClient = new SmtpClient();

            smptClient.Host = _emailSettings.Host;            
            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.UseDefaultCredentials = false;
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
            smptClient.EnableSsl = true;

            var message = new MailMessage();

            message.From = new MailAddress(_emailSettings.Username);
            message.To.Add(ToEmail);
            message.Subject = "Reset Password";
            message.Body = $"Click <a href='{resetmailLink}'>here</a> to reset your password.";
            message.IsBodyHtml = true;

            await smptClient.SendMailAsync(message);             
        }
    }
}
