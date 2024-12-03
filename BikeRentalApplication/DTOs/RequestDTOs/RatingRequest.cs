namespace BikeRentalApplication.DTOs.RequestDTOs
{
    public class RatingRequest
    {
        public Guid RecordId { get; set; }  
        public decimal? Rating { get; set; }
        public string? Review { get; set; }
    }
}
