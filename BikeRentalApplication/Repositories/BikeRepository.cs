using BikeRentalApplication.Database;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApplication.Repositories
{
    public class BikeRepository : IBikeRepository
    {
        private readonly RentalDbContext _dbContext;

        public BikeRepository(RentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Bike>> GetBike()
        {
            var bikes = await _dbContext.Bikes.ToListAsync();
            var data = await _dbContext.Bikes.Include(b => b.Images).Include(b => b.InventoryUnits).ToListAsync();
            return data;
        }

        public async Task<Bike> GetBike(Guid id)
        {
            var bike = await _dbContext.Bikes.Include(b => b.Images).Include(b => b.InventoryUnits).FirstOrDefaultAsync(b => b.Id == id);

            if (bike == null)
            {
                throw new Exception();
            }

            return bike;
        }

        public async Task<Bike> PutBike(Bike bike)
        {
            var updated = _dbContext.Bikes.Update(bike);
            await _dbContext.SaveChangesAsync();
          
            return updated.Entity;
        }

        public async Task<Bike> PostBike(Bike bike)
        {
           var data = await _dbContext.Bikes.AddAsync(bike);
            await _dbContext.SaveChangesAsync();

          return data.Entity;
        }

        public async Task<string> DeleteBike(Guid id)
        {
            var bike = await _dbContext.Bikes.FindAsync(id);
            if (bike == null)
            {
                throw new ArgumentException();
            }

            _dbContext.Bikes.Remove(bike);
            await _dbContext.SaveChangesAsync();

            return "Successfully Deleted...";
        }

        private bool BikeExists(Guid id)
        {
            return _dbContext.Bikes.Any(e => e.Id == id);
        }
    }
}
