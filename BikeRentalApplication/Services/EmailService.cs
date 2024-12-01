using BikeRentalApplication.DTOs.RequestDTOs;
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
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Bike Rental System", _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword); // Use the App Password here
            await smtp.SendAsync(emailMessage);
            await smtp.DisconnectAsync(true);
        }


        public async Task SendEmail(MailRequest mailRequest)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Bike Rental System", _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", mailRequest.User?.Email));
            emailMessage.Subject = mailRequest.Template.ToString();

            var bodyBuilder = new BodyBuilder { HtmlBody = MailBody(mailRequest) };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword); // Use the App Password here
            await smtp.SendAsync(emailMessage);
            await smtp.DisconnectAsync(true);
        }

        public string MailBody(MailRequest mailRequest)
        {
            if (mailRequest.Template == EmailTemplate.Registration)
            {
                return "";
            }
            else if (mailRequest.Template == EmailTemplate.Registration)
            {
                return "";
            }
            else if (mailRequest.Template == EmailTemplate.Registration)
            {
                return "";
            }
            else if (mailRequest.Template == EmailTemplate.Registration)
            {
                return "";
            }
            else
            {
                return "";
            }
        }
    }
}
