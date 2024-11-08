using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using BikeRentalApplication.IServices;

namespace BikeRentalApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task<User> UpdateUser(User user, Guid id)
        {
            return await _userRepository.UpdateUser(user);
        }
        public async Task<User> SignUp(UserRequest userRequest)
        {
            var user = new User
            {
                NICNumber = userRequest.NICNumber,
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,

                Email = userRequest.Email,
                ContactNo = userRequest.ContactNo,
                Address = userRequest.Address,
                HashPassword = userRequest.Password,
                AccountCreated = DateTime.Now,
                Role = userRequest.Role,
                IsBlocked = false,
                UserName = userRequest.UserName,
            };
            return await _userRepository.SignUp(user);
        }
        public async Task<string> DeleteUser(Guid id)
        {
            return await _userRepository.DeleteUser(id);
        }
    }
}
