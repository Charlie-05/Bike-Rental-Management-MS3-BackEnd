using BikeRentalApplication.Entities;

namespace BikeRentalApplication.DTOs.RequestDTOs
{
    public class RentalReqRequest
    {
        public DateTime RequestTime { get; set; }
        public Guid BikeId { get; set; }
        public string NICNumber { get; set; }
        //  public Guid RentalRecordId { get; set; }
    }
}
