using BikeRentalApplication.Database;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using BikeRentalApplication.IServices;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApplication.Services
{
    public class InventoryUnitService : IInventoryUnitService
    {

        private readonly IInventoryUnitRepository _inventoryUnitRepository;

        public InventoryUnitService(IInventoryUnitRepository invenroryUnitRepository)
        {

            _inventoryUnitRepository = invenroryUnitRepository;
        }

        public async Task<List<InventoryUnit>> GetInventoryUnits()
        {
            return await _inventoryUnitRepository.GetInventoryUnits();
        }

        public async Task<InventoryUnit> GetInventoryUnit(string registrationNumber)
        {
            var data = await _inventoryUnitRepository.GetInventoryUnit(registrationNumber);

      
            return data;
        }

        public async Task<InventoryUnit> PutInventoryUnit(InventoryUnit inventoryUnit)
        {
            var data = await _inventoryUnitRepository.PutInventoryUnit(inventoryUnit);
            return inventoryUnit;
        }

        public async Task<InventoryUnit> PostInventoryUnit(InventoryUnit inventoryUnit)
        {
            var data = await _inventoryUnitRepository.PostInventoryUnit(inventoryUnit);
            return inventoryUnit;
        }

        public async Task<string> DeleteInventoryUnit(string  registrationNumber)
        {
            var del = await _inventoryUnitRepository.GetInventoryUnit(registrationNumber);
            var data = await _inventoryUnitRepository.DeleteInventoryUnit(del);
            return data;
        }
    }
}
