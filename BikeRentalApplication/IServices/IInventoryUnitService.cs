using BikeRentalApplication.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalApplication.IServices
{
    public interface IInventoryUnitService
    {
        Task<List<InventoryUnit>> GetInventoryUnits();
        Task<InventoryUnit> GetInventoryUnit(Guid id);
        Task<InventoryUnit> PutInventoryUnit(InventoryUnit inventoryUnit);
        Task<InventoryUnit> PostInventoryUnit(InventoryUnit inventoryUnit);
        Task<string> DeleteInventoryUnit(Guid id);
    }
}
