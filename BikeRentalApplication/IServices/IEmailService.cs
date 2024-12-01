using BikeRentalApplication.DTOs.RequestDTOs;

namespace BikeRentalApplication.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
        Task SendEmail(MailRequest mailRequest);
    }
}
