using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalApplication.IServices
{
    public interface IInventoryUnitService
    {
        Task<List<InventoryUnit>> GetInventoryUnits(bool? availability , Guid? bikeId);
        Task<InventoryUnit> GetInventoryUnit(string RegistrationNumber);
        Task<InventoryUnit> PutInventoryUnit(InventoryUnit inventoryUnit);
        Task<List<InventoryUnit>> PostInventoryUnit(List<InventoryUnitRequest> inventoryUnits);
        Task<string> DeleteInventoryUnit(string registrationNumber);
    }
}
