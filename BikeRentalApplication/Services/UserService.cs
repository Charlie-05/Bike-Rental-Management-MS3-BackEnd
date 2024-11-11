using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using BikeRentalApplication.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BikeRentalApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<User> GetUser(string NICNo)
        {
            return await _userRepository.GetUser(NICNo);
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
                HashPassword = BCrypt.Net.BCrypt.HashPassword(userRequest.Password),
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

        public async Task<TokenModel> LogIn(LogInData logInData)
        {

                var user = await _userRepository.GetUser(logInData.NICNumber);
                var hash = BCrypt.Net.BCrypt.Verify(logInData.Password, user.HashPassword);
                if (hash)
                {
                    var token = CreateToken(user);
                    return token;
                }
                else
                {
                    throw new Exception("Invalid Password");
                }                  
        }

        private TokenModel CreateToken(User user)
        {
            var claimList = new List<Claim>();
            claimList.Add(new Claim("ContactNo", user.ContactNo));
            claimList.Add(new Claim("UserName", user.UserName));
            claimList.Add(new Claim("Email", user.Email));
            claimList.Add(new Claim("Role", user.Role.ToString()));

            var key = _configuration["JWT:Key"];
            var secKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claimList,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            var res = new TokenModel();
            res.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return res;
        }
    }
}
