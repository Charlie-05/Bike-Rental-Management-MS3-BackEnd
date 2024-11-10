using BikeRentalApplication.Database;
using BikeRentalApplication.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApplication.Repositories
{
    public class InventoryUnitRepository 
    {
        private readonly RentalDbContext _dbContext;

        public InventoryUnitRepository(RentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InventoryUnit>> GetInventoryUnits()
        {
            return await _dbContext.InventoryUnits.ToListAsync();
        }

        public async Task<InventoryUnit> GetInventoryUnit(Guid id)
        {
            var data = await _dbContext.InventoryUnits.FindAsync(id);

            if (data == null)
            {
                throw new Exception();
            }

            return data;
        }

        public async Task<InventoryUnit> PutInventoryUnit(InventoryUnit inventoryUnit)
        {
            _dbContext.Entry(inventoryUnit).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return inventoryUnit;
        }

        public async Task<InventoryUnit> PostInventoryUnit(InventoryUnit inventoryUnit)
        {
            var data = await _dbContext.InventoryUnits.AddAsync(inventoryUnit);
            await _dbContext.SaveChangesAsync();

            return data.Entity;
        }

        public async Task<string> DeleteInventoryUnit(Guid id)
        {
            var unit = await _dbContext.InventoryUnits.FindAsync(id);
            if (unit == null)
            {
                throw new ArgumentException();
            }

            _dbContext.InventoryUnits.Remove(unit);
            await _dbContext.SaveChangesAsync();

            return "Successfully Deleted...";
        }

    }
}
