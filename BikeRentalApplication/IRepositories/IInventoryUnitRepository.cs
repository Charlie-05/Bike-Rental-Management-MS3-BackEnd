using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IRepositories
{
    public interface IInventoryUnitRepository
    {
        Task<List<InventoryUnit>> GetInventoryUnits();
        Task<InventoryUnit> GetInventoryUnit(string RegistrationNumber);
        Task<InventoryUnit> PutInventoryUnit(InventoryUnit inventoryUnit);
        Task<List<InventoryUnit>> PostInventoryUnit(List<InventoryUnit> inventoryUnits);
        Task<string> DeleteInventoryUnit(InventoryUnit inventoryUnit);
    }
}
