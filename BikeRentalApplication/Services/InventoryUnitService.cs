using BikeRentalApplication.Database;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using BikeRentalApplication.IServices;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApplication.Services
{
    public class InventoryUnitService : IInventoryUnitService
    {

        private readonly IInvenroryUnitRepository _invenroryUnitRepository;

        public InventoryUnitService(IInvenroryUnitRepository invenroryUnitRepository)
        {
  
            _invenroryUnitRepository = invenroryUnitRepository;
        }

        public async Task<List<InventoryUnit>> GetInventoryUnits()
        {
            return await _invenroryUnitRepository.GetInventoryUnits();
        }

        public async Task<InventoryUnit> GetInventoryUnit(Guid id)
        {
            var data = await _invenroryUnitRepository.GetInventoryUnit(id);

      
            return data;
        }

        public async Task<InventoryUnit> PutInventoryUnit(InventoryUnit inventoryUnit)
        {
            var data = await _invenroryUnitRepository.PutInventoryUnit(inventoryUnit);
            return inventoryUnit;
        }

        public async Task<InventoryUnit> PostInventoryUnit(InventoryUnit inventoryUnit)
        {
            var data = await _invenroryUnitRepository.PostInventoryUnit(inventoryUnit);
            return inventoryUnit;
        }

        public async Task<string> DeleteInventoryUnit(Guid id)
        {
            var data = await _invenroryUnitRepository.DeleteInventoryUnit(id);
            return data;
        }
    }
}
