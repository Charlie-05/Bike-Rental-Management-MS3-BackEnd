using BikeRentalApplication.Entities;
using BikeRentalApplication.IServices;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BikeRentalApplication.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(User user, string subject)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Bike Rental System", _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", user.Email));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = RegisterMail(user) };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword); // Use the App Password here
            await smtp.SendAsync(emailMessage);
            await smtp.DisconnectAsync(true);
        }

        public string RegisterMail(User user)
        {
            return $"  <p>\r\n    Dear {user.FirstName} {user.LastName},\nThank you for registering in Bike rental Mangement.Setup your account to get your first ride...\r\n  " +
                $"For further inquires contact : +940766924093  </p>";
        }


    }
}
