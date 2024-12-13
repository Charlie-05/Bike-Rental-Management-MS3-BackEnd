using BikeRentalApplication.Database;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RentalDbContext _dbContext;

        public UserRepository(RentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetUsers(Roles? role)
        {
            return await _dbContext.Users.Where(u => u.Role == role).Include(u => u.RentalRequests).Include(u => u.RentalRecords).ToListAsync();
        }

        public async Task<User> GetUser(string NICNo)
        {
            var user = await _dbContext.Users.Include(u => u.RentalRequests).Include(u => u.RentalRecords).SingleOrDefaultAsync(u => u.NICNumber == NICNo);
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> SignUp(User user)
        {
            var data = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return data.Entity;
        }

        public async Task<string> DeleteUser(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return "Successfully Deleted...";
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<bool>UserNameExists(string userName)
        {
            return _dbContext.Users.Any(e => e.UserName == userName);
        }

        public async Task<List<User>> Search(string searchText)
        {
            var data = await _dbContext.Users.Where(b => b.NICNumber.Contains(searchText) || b.FirstName.Contains(searchText) || b.LastName.Contains(searchText)
            || b.Email.Contains(searchText) || b.ContactNo.Contains(searchText) || b.UserName.Contains(searchText)).Take(5).ToListAsync();
            return data;
        }

    }
}
