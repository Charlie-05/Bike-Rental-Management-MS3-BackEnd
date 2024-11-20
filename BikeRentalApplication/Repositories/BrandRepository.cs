using BikeRentalApplication.Database;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApplication.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private RentalDbContext _context;
        public BrandRepository(RentalDbContext context)
        {
            _context = context;
        }

        public async Task<Brand> AddBrand(Brand brand)
        {
            var data = await _context.Brands.AddAsync(brand);
            return data.Entity;
        }
        public async Task<List<Brand>> GetAllBrands()
        {
            var data = await _context.Brands.ToListAsync();
            return data;
        }
    }
}
