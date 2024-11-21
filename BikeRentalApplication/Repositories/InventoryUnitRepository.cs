using BikeRentalApplication.Database;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace BikeRentalApplication.Repositories
{
    public class InventoryUnitRepository : IInventoryUnitRepository
    {
        private readonly RentalDbContext _dbContext;

        public InventoryUnitRepository(RentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InventoryUnit>> GetInventoryUnits(bool? availability, Guid? bikeId)
        {
            if (availability == null || bikeId == null) {
                var data = await _dbContext.InventoryUnits.ToListAsync();
                return data;
            }
            return await _dbContext.InventoryUnits.Where(u => (u.Availability == availability) && (u.BikeId == bikeId)).ToListAsync();
           
        }

        public async Task<InventoryUnit> GetInventoryUnit(string RegistrationNumber)
        {
            var data = await _dbContext.InventoryUnits.SingleOrDefaultAsync(u => u.RegistrationNo == RegistrationNumber);

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

        public async Task<List<InventoryUnit>> PostInventoryUnit(List<InventoryUnit> inventoryUnits)
        {
             _dbContext.InventoryUnits.AddRangeAsync(inventoryUnits);
            await _dbContext.SaveChangesAsync();

            return inventoryUnits;
        }

        public async Task<string> DeleteInventoryUnit(InventoryUnit unit)
        {
            _dbContext.InventoryUnits.Remove(unit);
            await _dbContext.SaveChangesAsync();

            return "Successfully Deleted...";
        }

    }
}
