﻿using BikeRentalApplication.Database;
using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using BikeRentalApplication.IServices;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApplication.Services
{
    public class InventoryUnitService : IInventoryUnitService
    {

        private readonly IInventoryUnitRepository _inventoryUnitRepository;

        public InventoryUnitService(IInventoryUnitRepository inventoryUnitRepository)
        {

            _inventoryUnitRepository = inventoryUnitRepository;
        }

        public async Task<List<InventoryUnit>> GetInventoryUnits(bool? availability, Guid? bikeId)
        {
            return await _inventoryUnitRepository.GetInventoryUnits(availability , bikeId);
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

        public async Task<List<InventoryUnit>> PostInventoryUnit(List<InventoryUnitRequest> inventoryUnitRequests)
        {
         
            var inventoryUnits = inventoryUnitRequests.Select(u => new InventoryUnit
            {
                IsDeleted = false,
                RegistrationNo = u.RegistrationNo,
                YearOfManufacture = u.YearOfManufacture,
                DateAdded = DateTime.Now,
                Availability = true,
                BikeId = u.BikeId,
            }).ToList();
            var data = await _inventoryUnitRepository.PostInventoryUnit(inventoryUnits);
            return data;
        }

        public async Task<string> DeleteInventoryUnit(string  registrationNumber)
        {
            var del = await _inventoryUnitRepository.GetInventoryUnit(registrationNumber);
            if (del != null)
            {
                if (del.Availability == true)
                {
                    var data = await _inventoryUnitRepository.DeleteInventoryUnit(del);
                    return data;
                }
                else
                {
                    throw new Exception("Can not delete a bike on rent");
                }

            }
            else
            {
                return null;
            }

        }
    }
}
