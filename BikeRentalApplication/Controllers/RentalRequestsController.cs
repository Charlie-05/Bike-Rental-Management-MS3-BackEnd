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
    public class RentalRequestsController : ControllerBase
    {
        private readonly RentalDbContext _context;

        public RentalRequestsController(RentalDbContext context)
        {
            _context = context;
        }

        // GET: api/RentalRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalRequest>>> GetRentalRequest()
        {
            return await _context.RentalRequests.ToListAsync();
        }

        // GET: api/RentalRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalRequest>> GetRentalRequest(Guid id)
        {
            var rentalRequest = await _context.RentalRequests.FindAsync(id);

            if (rentalRequest == null)
            {
                return NotFound();
            }

            return rentalRequest;
        }

        // PUT: api/RentalRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRentalRequest(Guid id, RentalRequest rentalRequest)
        {
            if (id != rentalRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(rentalRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalRequestExists(id))
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

        // POST: api/RentalRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RentalRequest>> PostRentalRequest(RentalRequest rentalRequest)
        {
            _context.RentalRequests.Add(rentalRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRentalRequest", new { id = rentalRequest.Id }, rentalRequest);
        }

        // DELETE: api/RentalRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalRequest(Guid id)
        {
            var rentalRequest = await _context.RentalRequests.FindAsync(id);
            if (rentalRequest == null)
            {
                return NotFound();
            }

            _context.RentalRequests.Remove(rentalRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentalRequestExists(Guid id)
        {
            return _context.RentalRequests.Any(e => e.Id == id);
        }
    }
}
