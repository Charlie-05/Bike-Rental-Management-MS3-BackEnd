using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IRepositories
{
    public interface IInventoryUnitRepository
    {
        Task<List<InventoryUnit>> GetInventoryUnits();
        Task<InventoryUnit> GetInventoryUnit(string RegistrationNumber);
        Task<InventoryUnit> PutInventoryUnit(InventoryUnit inventoryUnit);
        Task<InventoryUnit> PostInventoryUnit(InventoryUnit inventoryUnit);
        Task<string> DeleteInventoryUnit(InventoryUnit inventoryUnit);
    }
}
