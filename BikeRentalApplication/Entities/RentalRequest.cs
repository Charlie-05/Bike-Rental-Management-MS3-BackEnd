namespace BikeRentalApplication.Entities
{
    public class RentalRequest
    {
        public Guid Id { get; set; }
        public DateTime RequestTime { get; set; }
        public Status Status { get; set; }
        public Bike? Bike { get; set; }
        public Guid BikeId { get; set; }
        public User? User { get; set; }
        public string UserId { get; set; }
        public RentalRecord? RentalRecord { get; set; }
      //  public Guid RentalRecordId { get; set; }
        public bool? Notify { get; set; }

    }

    public enum Status
    {
        Pending,
        Accepted,
        Declined,
        OnRent
    }
}
