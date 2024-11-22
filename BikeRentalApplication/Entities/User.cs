using System.ComponentModel.DataAnnotations;

namespace BikeRentalApplication.Entities
{
    public class User
    {
        [Key]
        public string NICNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string? HashPassword { get; set; }
        public DateTime AccountCreated { get; set; }
        public Roles Role { get; set; }
        public bool IsBlocked { get; set; }
        public string? UserName { get; set; }

        public List<RentalRequest>? RentalRequests { get; set; }
        public List<RentalRecord>? RentalRecords { get; set; }
        public string? ProfileImage {  get; set; }   
        public bool IsVerified { get; set; }
    }

    public enum Roles
    {
        Admin,
        Manager,
        User
    }
}
