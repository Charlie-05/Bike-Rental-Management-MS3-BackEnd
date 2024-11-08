﻿using BikeRentalApplication.Database;
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

        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUser(Guid id)
        {
            var bike = await _dbContext.Users.FindAsync(id);

            if (bike == null)
            {
                throw new Exception();
            }

            return bike;
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

        public async Task<string> DeleteUser(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new ArgumentException();
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return "Successfully Deleted...";
        }

    }
}
