using BikeRentalApplication.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalApplication.IServices
{
    public interface IInventoryUnitService
    {
        Task<List<InventoryUnit>> GetInventoryUnits();
        Task<InventoryUnit> GetInventoryUnit(string RegistrationNumber);
        Task<InventoryUnit> PutInventoryUnit(InventoryUnit inventoryUnit);
        Task<InventoryUnit> PostInventoryUnit(InventoryUnit inventoryUnit);
        Task<string> DeleteInventoryUnit(string registrationNumber);
    }
}
