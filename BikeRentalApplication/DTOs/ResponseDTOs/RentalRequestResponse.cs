using BikeRentalApplication.Entities;

namespace BikeRentalApplication.DTOs.ResponseDTOs
{
    public class RentalRequestResponse
    {
        public Guid Id { get; set; }
        public DateTime RequestTime { get; set; }
        public Status Status { get; set; }
        public Guid BikeId { get; set; }
        public string UserId { get; set; }
        //  public Guid RentalRecordId { get; set; }
        public bool? Notify { get; set; }
    }
}
