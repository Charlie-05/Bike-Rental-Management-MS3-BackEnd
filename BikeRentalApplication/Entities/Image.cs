using System.ComponentModel.DataAnnotations;

namespace BikeRentalApplication.Entities
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        [Required]  
        public string ImagePath { get; set; }
        public Bike? Bike { get; set; } 
        public Guid BikeId { get; set; }
    }
}
