using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.DTOs.ResponseDTOs;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using BikeRentalApplication.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BikeRentalApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IRentalRecordRepository _recordRepository;
        public UserService(IUserRepository userRepository, IConfiguration configuration, IRentalRecordRepository rentalRecordRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _recordRepository = rentalRecordRepository;
        }

        public async Task<List<User>> GetUsers(Roles? role)
        {
            return await _userRepository.GetUsers(role);
        }

        public async Task<UserResponse> GetUser(string NICNo)
        {
            var data = await _userRepository.GetUser(NICNo);
            var rentalRecords = new List<RentalRecord>();
            foreach (var item in data.RentalRequests)
            {
                var record = await _recordRepository.GetRentalRecordbyRequestID(item.Id);
<<<<<<< HEAD
<<<<<<< HEAD
                if(record != null)
                {
                    rentalRecords.Add(record);
                }
=======
=======
>>>>>>> f359cdfcb473f97815b0615579bebed27d3cdbb4
                if (record != null)
                {
                    rentalRecords.Add(record);
                }
               
<<<<<<< HEAD
>>>>>>> 9e8756a1110dbe45cb7ebd06f4517e78fb7aa386
=======
>>>>>>> f359cdfcb473f97815b0615579bebed27d3cdbb4
            }
              
            var response = new UserResponse
            {
                NICNumber = data.NICNumber,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                ContactNo = data.ContactNo,
                Address = data.Address,
                IsBlocked = data.IsBlocked,
                UserName = data.UserName,
                ProfileImage = data.ProfileImage,
                RentalRecords = rentalRecords.Select(r => new RentalRecordResponse
                {
                    Id = r.Id,
                    RentalOut = r.RentalOut,
                    RentalReturn = r.RentalReturn,
                    BikeRegNo = r.BikeRegNo,
                    Payment = r.Payment,
                    RentalRequestId = r.RentalRequestId,
                }).ToList(),
                RentalRequests = data.RentalRequests.Select(r => new RentalRequestResponse
                {Id = r.Id,
                RequestTime = r.RequestTime,
                Status = r.Status,
                BikeId = r.BikeId,
                UserId = r.UserId,
                Notify = r.Notify,

                }).ToList(),

            };
           
            
            if (data == null)
            {
                throw new Exception("Not found");
            }
            return response;
        }

        public async Task<User> UpdateUser(User user, string nicNo)
        {
            user.HashPassword = BCrypt.Net.BCrypt.HashPassword(user.HashPassword);
            return await _userRepository.UpdateUser(user);
        }
        public async Task<TokenModel> SignUp(UserRequest userRequest)
        {
            var user = new User
            {
                NICNumber = userRequest.NICNumber,
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                ContactNo = userRequest.ContactNo,
                Address = userRequest.Address,
                AccountCreated = DateTime.Now,
                Role = userRequest.Role,
                IsBlocked = false
            };
            if (userRequest.Role == Roles.Admin)
            {
                user.HashPassword = BCrypt.Net.BCrypt.HashPassword("Admin");
                user.UserName = "admin";
            }
            var getUser = await _userRepository.SignUp(user);
            if (getUser != null) {
                var token = CreateToken(user);
                return token;
            }
            else
            {
                throw new Exception("User Registration Failed");
            }
        }
        public async Task<string> DeleteUser(string nicNo)
        {
            var getUser = await _userRepository.GetUser(nicNo);
            if (getUser != null)
            {
                return await _userRepository.DeleteUser(getUser);
            }
            else
            {
                throw new Exception();
            }
            
        }

        public async Task<TokenModel> LogIn(LogInData logInData)
        {

            var user = await _userRepository.GetUserByUserName(logInData.UserName);
            if (user == null)
            {
                throw new Exception("Invalid Username");
            }
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

        public async Task<List<RoleResponse>> GetRoles()
        {
            var dict = new Dictionary<int, string>();
            foreach (var name in Enum.GetNames(typeof(Roles)))
            {
                dict.Add((int)Enum.Parse(typeof(Roles), name), name);
            }
            var roles = new List<RoleResponse>();
            foreach (var item in dict)
            {
                var role = new RoleResponse
                {
                    Key = item.Key,
                    Value = item.Value
                };
                roles.Add(role);
            }
            return roles;
        }
        private TokenModel CreateToken(User user)
        {
            var claimList = new List<Claim>();
            claimList.Add(new Claim("ContactNo", user.ContactNo));
            if(user.UserName != null)
            {
                claimList.Add(new Claim("UserName", user.UserName));
            }
            claimList.Add(new Claim("Email", user.Email));
            claimList.Add(new Claim("Role", user.Role.ToString()));
            claimList.Add(new Claim("NICNo", user.NICNumber));

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
            var res = new TokenModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return res;
        }
    }
}
