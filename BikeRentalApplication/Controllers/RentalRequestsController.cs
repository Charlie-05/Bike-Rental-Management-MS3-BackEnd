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
    public class RentalRequestsController : ControllerBase
    {
        private readonly RentalDbContext _context;
        private readonly IRentalRequestService _rentalRequestService;

        public RentalRequestsController(RentalDbContext context, IRentalRequestService rentalRequestService)
        {
            _context = context;
            _rentalRequestService = rentalRequestService;
        }

        // GET: api/RentalRequests
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRentalRequest(Status? status)
        {
            //  return await _context.RentalRequests.ToListAsync();
            try
            {
                var data = await _rentalRequestService.GetRentalRequests(status);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET: api/RentalRequests/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetRentalRequest(Guid id)
        {
            var rentalRequest = await _context.RentalRequests.FindAsync(id);

            if (rentalRequest == null)
            {
                return NotFound();
            }

            return Ok(rentalRequest);
        }

        // PUT: api/RentalRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> PostRentalRequest(RentalReqRequest rentalReqRequest)
        {
            try
            {
                var data = await _rentalRequestService.PostRentalRequest(rentalReqRequest);
                return Ok(data);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
           
        }

        // DELETE: api/RentalRequests/5
        [HttpDelete("{id}")]
        [Authorize]
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
        [HttpGet("Accept-Request{id}")]

        public async Task<IActionResult> AcceptRenatlRequest(Guid id)
        {
            var data = await _rentalRequestService.AcceptRentalRequest(id);
            return Ok(data);
        }
        [HttpGet("Decline-Request{id}")]
        public async Task<IActionResult> DeclineRenatlRequest(Guid id)
        {
            var data = await _rentalRequestService.DeclineRentalRequest(id);
            return Ok(data);
        }

        private bool RentalRequestExists(Guid id)
        {
            return _context.RentalRequests.Any(e => e.Id == id);
        }
    }
}
