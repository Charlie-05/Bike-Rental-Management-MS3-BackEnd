using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IRepositories
{
    public interface IIRentalRequestRepository
    {
        Task<List<InventoryUnit>> GetInventoryUnits();
        Task<InventoryUnit> GetInventoryUnit(Guid id);
        Task<InventoryUnit> PutInventoryUnit(InventoryUnit inventoryUnit);
        Task<InventoryUnit> PostInventoryUnit(InventoryUnit inventoryUnit);
        Task<string> DeleteInventoryUnit(Guid id);
    }
}
