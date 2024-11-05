using System.ComponentModel.DataAnnotations;

namespace BikeRentalApplication.Entities
{
    public class InventoryUnit
    {
        [Key]
        public string RegistrationNo { get; set; }
        public int? YearOfManufacture { get; set; }
        public bool Availability { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsDeleted { get; set; }
        public List<RentalRecord> RentalRecords { get; set; }
        public Bike? Bike { get; set; }
        public Guid BikeId { get; set; }
    }
}
