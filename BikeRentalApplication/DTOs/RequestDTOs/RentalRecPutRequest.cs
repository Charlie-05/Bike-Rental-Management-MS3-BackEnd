using BikeRentalApplication.Entities;

namespace BikeRentalApplication.DTOs.RequestDTOs
{
    public class RentalRecPutRequest
    {
        public Guid Id { get; set; }
        public decimal? Payment { get; set; }

    }
}
