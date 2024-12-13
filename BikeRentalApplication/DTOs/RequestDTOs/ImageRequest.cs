namespace BikeRentalApplication.DTOs.RequestDTOs
{
    public class ImageRequest
    {
        public Guid? BikeId { get; set; }
        public string ImagePath { get; set; }
    }
}
