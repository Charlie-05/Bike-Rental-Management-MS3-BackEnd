using BikeRentalApplication.Database;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApplication.Repositories
{
    public class BikeRepository : IBikeRepository
    {
        private readonly RentalDbContext _dbContext;

        public BikeRepository(RentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Bike>> GetBike()
        {
            return await _dbContext.Bikes.ToListAsync();
        }

        public async Task<Bike> GetBike(Guid id)
        {
            var bike = await _dbContext.Bikes.FindAsync(id);

            if (bike == null)
            {
                throw new Exception();
            }

            return bike;
        }

        public async Task<Bike> PutBike(Bike bike)
        {
            //if (id != bike.Id)
            //{
            //    return BadRequest();
            //}

            _dbContext.Entry(bike).State = EntityState.Modified;

            //try
            //{
            await _dbContext.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!BikeExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
            return bike;
        }

        public async Task<Bike> PostBike(Bike bike)
        {
           var data = await _dbContext.Bikes.AddAsync(bike);
            await _dbContext.SaveChangesAsync();

          //  return CreatedAtAction("GetBike", new { id = bike.Id }, bike);
          return data.Entity;
        }

        public async Task<string> DeleteBike(Guid id)
        {
            var bike = await _dbContext.Bikes.FindAsync(id);
            if (bike == null)
            {
                //  return NotFound();
                throw new ArgumentException();
            }

            _dbContext.Bikes.Remove(bike);
            await _dbContext.SaveChangesAsync();

            return "Successfully Deleted...";
        }

        private bool BikeExists(Guid id)
        {
            return _dbContext.Bikes.Any(e => e.Id == id);
        }
    }
}
