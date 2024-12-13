using BikeRentalApplication.DTOs.ResponseDTOs;
using BikeRentalApplication.Entities;

namespace BikeRentalApplication.DTOs.RequestDTOs
{
    public class MailRequest
    {
        public User? User { get; set; }
      //  public PaymentResponse? Payment { get; set; }
        public EmailTemplate Template { get; set; }
        public RentalRecord? RentalRecord { get; set; }
        public RentalRequest? RentalRequest { get; set; }
        public Bike? Bike { get; set; }
    }
}
