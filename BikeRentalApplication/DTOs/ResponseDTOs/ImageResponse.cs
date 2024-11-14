using BikeRentalApplication.Entities;

namespace BikeRentalApplication.DTOs.ResponseDTOs
{
    public class ImageResponse
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public Guid BikeId { get; set; }
    }
}
