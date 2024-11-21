namespace BikeRentalApplication.Entities
{
    public class RentalRecord
    {
        public Guid Id { get; set; }
        public DateTime? RentalOut { get; set; }
        public DateTime? RentalReturn { get; set; }
        public decimal? Payment { get; set; }
        public string? BikeRegNo { get; set; }   
        public InventoryUnit? InventoryUnit { get; set; }
        public RentalRequest? RentalRequest { get; set; }
        public Guid RentalRequestId { get; set; }
        public string? Review {  get; set; }
    }

    public enum State
    {
        Incompleted = 0,
        Completed = 1
    }
}
