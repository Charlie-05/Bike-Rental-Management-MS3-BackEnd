using System.ComponentModel.DataAnnotations;

namespace BikeRentalApplication.Entities
{
    public class Bike
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public decimal RatePerHour { get; set; }
        public List<InventoryUnit>? InventoryUnits {  get; set; }
        public List<RentalRequest>? RentalRequests { get; set; }
        public List<Image>? Images { get; set; }
        public decimal Rating { get; set; }
        public string? Description { get; set; }

    }
}
