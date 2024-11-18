using BikeRentalApplication.Entities;

namespace BikeRentalApplication.DTOs.ResponseDTOs
{
    public class RentalRecordResponse
    {
        public Guid Id { get; set; }
        public DateTime? RentalOut { get; set; }
        public DateTime? RentalReturn { get; set; }
        public decimal? Payment { get; set; }
        public string? BikeRegNo { get; set; }
        public Guid RentalRequestId { get; set; }
    }
}
