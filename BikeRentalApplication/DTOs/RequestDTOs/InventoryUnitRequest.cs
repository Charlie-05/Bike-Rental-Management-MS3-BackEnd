using BikeRentalApplication.Entities;

namespace BikeRentalApplication.DTOs.RequestDTOs
{
    public class InventoryUnitRequest
    {
        public string RegistrationNo { get; set; }
        public int? YearOfManufacture { get; set; }
        public Guid BikeId { get; set; }

    }
}
