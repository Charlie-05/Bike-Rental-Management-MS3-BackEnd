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
                return "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n" +
                    "    <meta charset=\"UTF-8\">\r\n   " +
                    " <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n  " +
                    "  <style>\r\n    " +
                    "    body {\r\n     " +
                    "       font-family: Arial, sans-serif;\r\n     " +
                    "       margin: 0;\r\n            padding: 0;\r\n  " +
                    "          background-color: #f4f4f4;\r\n        }\r\n " +
                    "       .email-container {\r\n            max-width: 600px;\r\n  " +
                    "          margin: 20px auto;\r\n            background: #ffffff;\r\n  " +
                    "          border-radius: 8px;\r\n            overflow: hidden;\r\n " +
                    "           box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);\r\n        }\r\n  " +
                    "      .header {\r\n            background-color: #007BFF;\r\n   " +
                    "         color: #ffffff;\r\n            padding: 20px;\r\n      " +
                    "      text-align: center;\r\n        }\r\n   " +
                    "     .header h1 {\r\n            margin: 0;\r\n       " +
                    "     font-size: 24px;\r\n        }\r\n        .content {\r\n            padding: 20px;\r\n  " +
                    "          color: #333333;\r\n        }\r\n   " +
                    "     .content p {\r\n            margin: 10px 0;\r\n     " +
                    "       line-height: 1.6;\r\n        }\r\n  " +
                    "      .cta-button {\r\n      " +
                    "      display: inline-block;\r\n      " +
                    "      background-color: #007BFF;\r\n            color: #ffffff;\r\n          " +
                    "  padding: 10px 20px;\r\n            text-decoration: none;\r\n           " +
                    " border-radius: 5px;\r\n            margin-top: 20px;\r\n        }\r\n       " +
                    " .footer {\r\n            background-color: #f4f4f4;\r\n            text-align: center;\r\n    " +
                    "        padding: 10px;\r\n            font-size: 12px;\r\n            color: #666666;\r\n        }" +
                    "\r\n        .footer a {\r\n            color: #007BFF;\r\n    " +
                    "        text-decoration: none;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n  " +
                    "  <div class=\"email-container\">\r\n        <div class=\"header\">\r\n            " +
                    "<h1>Welcome to RentWheelz!</h1>\r\n        </div>\r\n        <div class=\"content\">\r\n    " +
                    $"        <p>Hi {mailRequest.User.FirstName} {mailRequest.User.LastName},</p>\r\n            <p>Thank you for registering with RideOn Rentals!" +
                    " We are excited to have you on board. With our service, you can explore a wide range of bikes for your" +
                    " adventures or daily needs.</p>\r\n            <p>Your account has been successfully created. You can" +
                    " now log in to your account and start booking your rides.</p>\r\n            <p>If you have any questions" +
                    " or need assistance, feel free to contact us at <a href=\"mailto:support@rideonrentals.com\">support@rideonrentals.com</a>.</p>\r\n" +
                    "            <a href=\"https://yourwebsite.com/login\" class=\"cta-button\">Log in to Your Account</a>\r\n   " +
                    "     </div>\r\n        <div class=\"footer\">\r\n            <p>&copy; 2024 RideOn Rentals. All rights reserved.</p>\r\n  " +
                    "          <p>Need help? Visit our <a href=\"https://yourwebsite.com/support\">Support Center</a>.</p>\r\n        </div>\r\n " +
                    "   </div>\r\n</body>\r\n</html>\r\n";
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
