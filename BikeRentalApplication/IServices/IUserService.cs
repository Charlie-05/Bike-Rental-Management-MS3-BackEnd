using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.DTOs.ResponseDTOs;
using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetUsers(Roles? role);
        Task<User> GetUser(string NICNo);
        Task<User> UpdateUser(User user, string nicNo);
        Task<TokenModel> SignUp(UserRequest userRequest);
        Task<string> DeleteUser(Guid id);
        Task<TokenModel> LogIn(LogInData logInData);
        Task<List<RoleResponse>> GetRoles();

    }
}
