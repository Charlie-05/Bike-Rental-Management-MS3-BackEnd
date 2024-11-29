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
            data.RentalRequests = data.RentalRequests ?? [];
            var rentalRecords = new List<RentalRecord>();
            foreach (var item in data.RentalRequests)
            {
                var record = await _recordRepository.GetRentalRecordbyRequestID(item.Id);

                if (record != null)
                {
                    rentalRecords.Add(record);
                }


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
                Role = data.Role,
                AccountCreated = data.AccountCreated,
                RentalRecords = rentalRecords.Select(r => new RentalRecordResponse
                {
                    Id = r.Id,
                    RentalOut = r.RentalOut,
                    RentalReturn = r.RentalReturn,
                    BikeRegNo = r.BikeRegNo,
                    Payment = r.Payment,
                    RentalRequestId = r.RentalRequestId,
                }).OrderByDescending(r => r.RentalOut).ToList(),
                RentalRequests = data.RentalRequests.Select(r => new RentalRequestResponse
                {
                    Id = r.Id,
                    RequestTime = r.RequestTime,
                    Status = r.Status,
                    BikeId = r.BikeId,
                    UserId = r.UserId,
                    Notify = r.Notify,

                }).OrderByDescending(r => r.RequestTime).ToList(),

            };


            if (data == null)
            {
                throw new Exception("Not found");
            }
            return response;
        }

        public async Task<User> UpdateUser(UserPutRequest userRequest, string nicNo, Settings setting)
        {
            var userNameUnavailable = await _userRepository.UserNameExists(userRequest.UserName);
            if (userNameUnavailable)
            {
                throw new Exception("Username already exists.Try something different.");
            }
            var user = await _userRepository.GetUser(nicNo);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (setting == Settings.credentials)
            {
                user.UserName = userRequest.UserName;
                user.HashPassword = BCrypt.Net.BCrypt.HashPassword(userRequest.HashPassword);
                if (user.Role == Roles.Admin)
                {
                    user.IsVerified = true;
                }
                if (user.Role == Roles.Manager && user.IsVerified == false)
                {
                    throw new Exception("Your registration is under approval by Admin.");
                }
            }
            else if (setting == Settings.info)
            {
                user.FirstName = userRequest.FirstName;
                user.LastName = userRequest.LastName;
                user.ProfileImage = userRequest.ProfileImage;
                user.Address = userRequest.Address;
                user.Email = userRequest.Email;
                user.ContactNo = userRequest.ContactNo;
            }


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
            if (getUser != null)
            {
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

        public async Task<User> VerifyUser(string nicNo)
        {
            var getUser = await _userRepository.GetUser(nicNo);
            if (getUser == null)
            {
                throw new Exception("User not Found");
            }
            getUser.IsVerified = true;
            var data = await _userRepository.UpdateUser(getUser);
            return data;
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
                throw new Exception("Check your password");
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
            if (user.UserName != null)
            {
                claimList.Add(new Claim("UserName", user.UserName));
            }
            claimList.Add(new Claim("Email", user.Email));
            claimList.Add(new Claim("Role", user.Role.ToString()));
            claimList.Add(new Claim("NICNo", user.NICNumber));
            claimList.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));

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
