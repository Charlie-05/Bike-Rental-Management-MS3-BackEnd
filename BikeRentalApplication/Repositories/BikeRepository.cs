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
            var data = await _dbContext.Bikes.Include(b => b.Images).Include(b => b.InventoryUnits).Include(b => b.Brand).ToListAsync();
            return data;
        }
        public async Task<List<Bike>> GetBikeTypeFilter(string? type)
        {
            var data = await _dbContext.Bikes.Where(b => b.Type == type).Include(b => b.Images)
                .Include(b => b.InventoryUnits).Include(b => b.Brand).ToListAsync();
            return data;
        }
        public async Task<List<Bike>> GetBikeBrandFilter(Guid? brandId)
        {
            var data = await _dbContext.Bikes.Where(b => b.BrandId == brandId).Include(b => b.Images)
                .Include(b => b.InventoryUnits).Include(b => b.Brand).ToListAsync();
            return data;
        }
        public async Task<List<Bike>> GetBikeFilter(string? type , Guid? brandId)
        {
            var data = await _dbContext.Bikes.Where(b => b.Type == type && b.BrandId == brandId).Include(b => b.Images)
                .Include(b => b.InventoryUnits).Include(b=> b.Brand).ToListAsync();
            return data;
        }

        public async Task<Bike> GetBike(Guid id)
        {
            var bike = await _dbContext.Bikes.Include(b => b.Images).Include(b => b.InventoryUnits).Include(b => b.Brand).FirstOrDefaultAsync(b => b.Id == id);

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

        public async Task<Bike> DeleteBike(Bike bike)
        {
         _dbContext.Bikes.Remove(bike);
            var data = await _dbContext.SaveChangesAsync();
            return bike;
            
        }

        private bool BikeExists(Guid id)
        {
            return _dbContext.Bikes.Any(e => e.Id == id);
        }
    }
}
