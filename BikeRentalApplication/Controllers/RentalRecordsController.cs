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
    public class RentalRecordsController : ControllerBase
    {
        private readonly RentalDbContext _context;

        public RentalRecordsController(RentalDbContext context)
        {
            _context = context;
        }

        // GET: api/RentalRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalRecord>>> GetRentalRecord()
        {
            return await _context.RentalRecords.ToListAsync();
        }

        // GET: api/RentalRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalRecord>> GetRentalRecord(Guid id)
        {
            var rentalRecord = await _context.RentalRecords.FindAsync(id);

            if (rentalRecord == null)
            {
                return NotFound();
            }

            return rentalRecord;
        }

        // PUT: api/RentalRecords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRentalRecord(Guid id, RentalRecord rentalRecord)
        {
            if (id != rentalRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(rentalRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalRecordExists(id))
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

        // POST: api/RentalRecords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RentalRecord>> PostRentalRecord(RentalRecord rentalRecord)
        {
            _context.RentalRecords.Add(rentalRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRentalRecord", new { id = rentalRecord.Id }, rentalRecord);
        }

        // DELETE: api/RentalRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalRecord(Guid id)
        {
            var rentalRecord = await _context.RentalRecords.FindAsync(id);
            if (rentalRecord == null)
            {
                return NotFound();
            }

            _context.RentalRecords.Remove(rentalRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentalRecordExists(Guid id)
        {
            return _context.RentalRecords.Any(e => e.Id == id);
        }
    }
}
