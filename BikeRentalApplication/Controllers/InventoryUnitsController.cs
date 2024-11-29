using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeRentalApplication.Database;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IServices;
using BikeRentalApplication.DTOs.RequestDTOs;
using Microsoft.AspNetCore.Authorization;

namespace BikeRentalApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryUnitsController : ControllerBase
    {
        private readonly IInventoryUnitService _inventoryUnitService;

        public InventoryUnitsController(IInventoryUnitService inventoryUnitService)
        {
            _inventoryUnitService = inventoryUnitService;
        }

        // GET: api/InventoryUnits
        [HttpGet]
        public async Task<IActionResult> GetInventoryUnit(bool? availability, Guid? bikeId)
        {
            try
            {
                var data = await _inventoryUnitService.GetInventoryUnits(availability , bikeId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET: api/InventoryUnits/5
        [HttpGet("{registrationNumber}")]

        public async Task<IActionResult> GetInventoryUnit(string registrationNumber)
        {
            try
            {
                var data = await _inventoryUnitService.GetInventoryUnit(registrationNumber);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // PUT: api/InventoryUnits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutInventoryUnit(string RegistrationNumber, InventoryUnit inventoryUnit)
        //{
        //    if (id != inventoryUnit.RegistrationNo)
        //    {
        //        return BadRequest();
        //    }


        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!InventoryUnitExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/InventoryUnits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostInventoryUnit(List<InventoryUnitRequest> inventoryUnitRequests)
        {
            try
            {
                var data = await _inventoryUnitService.PostInventoryUnit(inventoryUnitRequests);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //    // DELETE: api/InventoryUnits/5
        [HttpDelete("{registrationNumber}")]
        public async Task<IActionResult> DeleteInventoryUnit(string registrationNumber)
        {
            try
            {
                var res = await _inventoryUnitService.DeleteInventoryUnit(registrationNumber);
                return Ok(res);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }               
        }

        //private bool InventoryUnitExists(string id)
        //{
        //    return _context.InventoryUnits.Any(e => e.RegistrationNo == id);
        //}
    }
}
