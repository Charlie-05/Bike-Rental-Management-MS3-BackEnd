using BikeRentalApplication.Database;
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
            return await _dbContext.RentalRequests.ToListAsync();
        }

        public async Task<RentalRequest> GetRequest(Guid id)
        {
            var request = await _dbContext.RentalRequests.SingleOrDefaultAsync(u => u.Id == id);

            if (request == null)
            {
                throw new Exception();
            }

            return request;
        }

        public async Task<RentalRequest> UpdateRequest(RentalRequest rentalRequest)
        {
            _dbContext.Entry(rentalRequest).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return rentalRequest;
        }

        public async Task<string> DeleteRequest(Guid id)
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
    }
}
