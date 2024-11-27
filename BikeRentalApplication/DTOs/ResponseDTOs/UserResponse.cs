using BikeRentalApplication.Entities;
using System.ComponentModel.DataAnnotations;

namespace BikeRentalApplication.DTOs.ResponseDTOs
{
    public class UserResponse
    {
        public string NICNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public Roles Role { get; set; }
        public bool IsBlocked { get; set; }
        public string? UserName { get; set; }
        public List<RentalRequestResponse>? RentalRequests { get; set; }
        public List<RentalRecordResponse>? RentalRecords { get; set; }
        public string? ProfileImage { get; set; }
        public DateTime AccountCreated { get; set; }
    }
}
