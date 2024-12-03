using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IServices
{
    public interface IEmailService
    {
        //Task SendEmailAsync(string toEmail, string subject, string body);
        Task SendEmailAsync(User  user, string subject);
       // Task Subscribe(EmailRequest emailRequest);
    }
}
