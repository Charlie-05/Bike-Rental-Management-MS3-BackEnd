using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(Guid id);
        Task<User> UpdateUser(User user, Guid id);
        Task<User> SignUp(UserRequest userRequest);
        Task<string> DeleteUser(Guid id);

    }
}
