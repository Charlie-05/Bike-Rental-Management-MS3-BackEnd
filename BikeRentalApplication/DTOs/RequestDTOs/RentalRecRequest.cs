using BikeRentalApplication.Entities;

namespace BikeRentalApplication.DTOs.RequestDTOs
{
    public class RentalRecRequest
    {
        public DateTime? RentalOut { get; set; }
        public string? BikeRegNo { get; set; }
        public Guid RentalRequestId { get; set; }
    }
}
