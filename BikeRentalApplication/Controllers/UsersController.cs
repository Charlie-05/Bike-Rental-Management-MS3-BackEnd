using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeRentalApplication.Database;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IServices;
using BikeRentalApplication.DTOs.RequestDTOs;

namespace BikeRentalApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RentalDbContext _context;
        private readonly IUserService _userService;

        public UsersController(RentalDbContext context , IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers(Roles? role)
        {
            var data = await _userService.GetUsers(role);
            return Ok(data);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{nicNo}")]
        public async Task<IActionResult> PutUser(User user, string nicNo)
        {


            try
            {
               var data = await  _userService.UpdateUser(user, nicNo);  
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

       
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.NICNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.NICNumber }, user);
        }

        [HttpPost("Sign-Up")]
        public async Task<ActionResult<User>> SignUp(UserRequest userRequest)
        {
        
            //try
            //{
                var data = await _userService.SignUp(userRequest);
                return Ok(data);
           // }
            //catch (Exception ex) {
            //    return BadRequest(ex.Message);
            //}
          
        }

        [HttpPost("Log-In")]
        public async Task<ActionResult<User>> LogIn(LogInData logInData)
        {

            try
            {
                var data = await _userService.LogIn(logInData);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var data = await _userService.DeleteUser(id);
                return Ok(data);
            }catch(Exception ex){
                return BadRequest(ex.Message);  
            }
          
        }

        [HttpGet("Get-Roles")]
        public async Task<IActionResult> GetRoles()
        {
            var data = await _userService.GetRoles();
            return Ok(data);
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.NICNumber == id);
        }
    }
}
