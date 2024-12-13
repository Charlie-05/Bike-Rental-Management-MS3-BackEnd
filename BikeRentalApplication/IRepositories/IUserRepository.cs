using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers(Roles? role);
        Task<User> GetUser(string NICNo);
        Task<User> UpdateUser(User user);
        Task<User> SignUp(User user);
        Task<string> DeleteUser(User user);
        Task<User> GetUserByUserName(string userName);
        Task<bool> UserNameExists(string userName);
        Task<List<User>> Search(string searchText);
    }
}
