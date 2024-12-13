using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.DTOs.ResponseDTOs;
using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetUsers(Roles? role);
        Task<UserResponse> GetUser(string NICNo);
        Task<User> UpdateUser(UserPutRequest? user, string nicNo, Settings Settings);
        Task<TokenModel> SignUp(UserRequest userRequest);
        Task<string> DeleteUser(string nicNo);
        Task<TokenModel> LogIn(LogInData logInData);
        Task<List<RoleResponse>> GetRoles();
        Task<User> VerifyUser(string nicNo);

    }
}
