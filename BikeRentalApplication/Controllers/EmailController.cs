using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        //[HttpPost("send")]
        //public async Task<IActionResult> SendEmail(EmailRequest request)
        //{
        //    try
        //    {
        //        await _emailService.SendEmailAsync(request.ToEmail, request.Subject, request.Body);
        //        return Ok("Email sent successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
    }
}
