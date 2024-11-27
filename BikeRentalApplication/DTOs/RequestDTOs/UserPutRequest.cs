namespace BikeRentalApplication.DTOs.RequestDTOs
{
    public class UserPutRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string? HashPassword { get; set; }
        public string? UserName { get; set; }
        public string? ProfileImage { get; set; }
    }
}
