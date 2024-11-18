using BikeRentalApplication.Entities;

namespace BikeRentalApplication.DTOs.RequestDTOs
{
    public class RentalRecRequest
    {
        public string? BikeRegNo { get; set; }
        public Guid RentalRequestId { get; set; }
    }
}
