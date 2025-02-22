﻿using BikeRentalApplication.Database;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApplication.Repositories
{
    public class RentalRequestRepository : IRentalRequestRepository
    {
        private readonly RentalDbContext _dbContext;

        public RentalRequestRepository(RentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<RentalRequest> PostRentalRequest(RentalRequest rentalRequest)
        {
            var data = await _dbContext.RentalRequests.AddAsync(rentalRequest);
            await _dbContext.SaveChangesAsync();

            return data.Entity;
        }
        public async Task<List<RentalRequest>> GetRentalRequests()
        {
            return await _dbContext.RentalRequests.Where(r => r.Status == Status.Pending).Include(r => r.Bike).ThenInclude(b => b.Brand).ToListAsync();
        }

        public async Task<List<RentalRequest>> GetRentalRequestsByStatus(Status? status)
        {
            return await _dbContext.RentalRequests.Where(r => r.Status == status).Include(r => r.Bike).ThenInclude(r => r.Brand).ToListAsync();
        }

        public async Task<RentalRequest> GetRentalRequest(Guid id)
        {
            var request = await _dbContext.RentalRequests.Include(r => r.User).Include(r => r.Bike).ThenInclude(r => r.Brand).SingleOrDefaultAsync(u => u.Id == id);

            if (request == null)
            {
                throw new Exception();
            }

            return request;
        }


        public async Task<RentalRequest> UpdateRentalRequest(RentalRequest rentalRequest)
        {
            var data = _dbContext.RentalRequests.Update(rentalRequest);
            await _dbContext.SaveChangesAsync();

            return data.Entity;
        }

        public async Task<string> DeleteRentalRequest(Guid id)
        {
            var request = await _dbContext.RentalRequests.FindAsync(id);
            if (request == null)
            {
                throw new ArgumentException();
            }

            _dbContext.RentalRequests.Remove(request);
            await _dbContext.SaveChangesAsync();

            return "Successfully Deleted...";
        }
        public async Task<List<RentalRequest>> Search(string searchText)
        {
            var data = await _dbContext.RentalRequests.Where(b => b.UserId.Contains(searchText) || b.RequestTime.ToString().Contains(searchText)).Take(5).ToListAsync();
            return data;
        }
    }
}
