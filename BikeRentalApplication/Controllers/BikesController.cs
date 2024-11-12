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
    public class BikesController : ControllerBase
    {
        private readonly RentalDbContext _context;
        private readonly IBikeService _bikeService;

        public BikesController(RentalDbContext context, IBikeService bikeService)
        {
            _context = context;
            _bikeService = bikeService;
        }

        // GET: api/Bikes
        [HttpGet]
        public async Task<ActionResult<Bike>> GetBike()
        {
            try
            {
                var data = await _bikeService.GetBike();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // GET: api/Bikes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bike>> GetBike(Guid id)
        {
            try
            {
                var data = await _bikeService.GetBike(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/Bikes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBike(Guid id, Bike bike)
        {
            if (id != bike.Id)
            {
                return BadRequest();
            }
            // _context.Entry(bike).State = EntityState.Modified;          

            try
            {
                var data = await _bikeService.PutBike(bike, id);
                return Ok(data);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BikeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: api/Bikes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostBike(Bike bike)
        {

            var data = await _bikeService.PostBike(bike);
            return Ok(data);
        }

        // DELETE: api/Bikes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBike(Guid id)
        {

            try
            {
                var data = await _bikeService.DeleteBike(id);
                return Ok(data);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            // return NoContent();
        }

        private bool BikeExists(Guid id)
        {
            return _context.Bikes.Any(e => e.Id == id);
        }
    }
}
