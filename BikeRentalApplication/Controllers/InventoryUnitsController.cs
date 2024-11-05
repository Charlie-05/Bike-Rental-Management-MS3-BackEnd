using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeRentalApplication.Database;
using BikeRentalApplication.Entities;

namespace BikeRentalApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryUnitsController : ControllerBase
    {
        private readonly RentalDbContext _context;

        public InventoryUnitsController(RentalDbContext context)
        {
            _context = context;
        }

        // GET: api/InventoryUnits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryUnit>>> GetInventoryUnit()
        {
            return await _context.InventoryUnit.ToListAsync();
        }

        // GET: api/InventoryUnits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryUnit>> GetInventoryUnit(string id)
        {
            var inventoryUnit = await _context.InventoryUnit.FindAsync(id);

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
        public async Task<ActionResult<InventoryUnit>> PostInventoryUnit(InventoryUnit inventoryUnit)
        {
            _context.InventoryUnit.Add(inventoryUnit);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InventoryUnitExists(inventoryUnit.RegistrationNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInventoryUnit", new { id = inventoryUnit.RegistrationNo }, inventoryUnit);
        }

        // DELETE: api/InventoryUnits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryUnit(string id)
        {
            var inventoryUnit = await _context.InventoryUnit.FindAsync(id);
            if (inventoryUnit == null)
            {
                return NotFound();
            }

            _context.InventoryUnit.Remove(inventoryUnit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryUnitExists(string id)
        {
            return _context.InventoryUnit.Any(e => e.RegistrationNo == id);
        }
    }
}
