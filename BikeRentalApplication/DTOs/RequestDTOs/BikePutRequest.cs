namespace BikeRentalApplication.DTOs.RequestDTOs
{
    public class BikePutRequest
    {
        public Guid BrandId { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public decimal RatePerHour { get; set; }
    }
}
