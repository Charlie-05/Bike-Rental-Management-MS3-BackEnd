using BikeRentalApplication.Entities;

namespace BikeRentalApplication.DTOs.ResponseDTOs
{
    public class BikeResponse
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public decimal RatePerHour { get; set; }
        public List<InventoryUnit> InventoryUnits { get; set; }
        public List<RentalRequest> RentalRequests { get; set; }
        public List<Image> Images { get; set; }
    }
}
