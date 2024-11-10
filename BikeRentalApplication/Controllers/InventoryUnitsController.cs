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

namespace BikeRentalApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryUnitsController : ControllerBase
    {
        private readonly RentalDbContext _context;
        private readonly IInventoryUnitService _inventoryUnitService;

        public InventoryUnitsController(RentalDbContext context , IInventoryUnitService inventoryUnitService)
        {
            _context = context;
            _inventoryUnitService = inventoryUnitService;
        }

        // GET: api/InventoryUnits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryUnit>>> GetInventoryUnit()
        {
            return await _context.InventoryUnits.ToListAsync();
        }

        // GET: api/InventoryUnits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryUnit>> GetInventoryUnit(string id)
        {
            var inventoryUnit = await _context.InventoryUnits.FindAsync(id);

            if (inventoryUnit == null)
            {
                return NotFound();
            }

            return inventoryUnit;
        }

        // PUT: api/InventoryUnits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryUnit(string id, InventoryUnit inventoryUnit)
        {
            if (id != inventoryUnit.RegistrationNo)
            {
                return BadRequest();
            }

            _context.Entry(inventoryUnit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryUnitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InventoryUnits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostInventoryUnit(InventoryUnit inventoryUnit)
        {
            //_context.InventoryUnits.Add(inventoryUnit);
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    if (InventoryUnitExists(inventoryUnit.RegistrationNo))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return CreatedAtAction("GetInventoryUnit", new { id = inventoryUnit.RegistrationNo }, inventoryUnit);
            try
            {
                var data = await _inventoryUnitService.PostInventoryUnit(inventoryUnit);
                return Ok(data);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/InventoryUnits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryUnit(string id)
        {
            var inventoryUnit = await _context.InventoryUnits.FindAsync(id);
            if (inventoryUnit == null)
            {
                return NotFound();
            }

            _context.InventoryUnits.Remove(inventoryUnit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryUnitExists(string id)
        {
            return _context.InventoryUnits.Any(e => e.RegistrationNo == id);
        }
    }
}
