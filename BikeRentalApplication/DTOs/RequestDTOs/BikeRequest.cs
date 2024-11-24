namespace BikeRentalApplication.DTOs.RequestDTOs
{
    public class BikeRequest
    {
        public Guid BrandId { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public decimal RatePerHour { get; set; }
        public List<ImageRequest>? Images { get; set; }
        public string? Description { get; set; }
    }
}
