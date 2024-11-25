using BikeRentalApplication.Entities;

namespace BikeRentalApplication.DTOs.ResponseDTOs
{
    public class BikeResponse
    {
        public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public BrandResponse Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public decimal RatePerHour { get; set; }
        public string? Description { get; set; }
        public decimal Rating { get; set; }
        public int NumberOfRatings { get; set; }
        public List<InventoryUnitResponse> InventoryUnits { get; set; }
        public List<RentalRequest> RentalRequests { get; set; }
        public List<ImageResponse> Images { get; set; }
    }
}
