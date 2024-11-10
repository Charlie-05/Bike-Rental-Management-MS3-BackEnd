using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(string NICNo);
        Task<User> UpdateUser(User user);
        Task<User> SignUp(User user);
        Task<string> DeleteUser(Guid id);
    }
}
