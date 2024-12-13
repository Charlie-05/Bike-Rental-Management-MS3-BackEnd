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
           await _context.SaveChangesAsync();
            return data.Entity;
        }
        public async Task<List<Brand>> GetAllBrands()
        {
            var data = await _context.Brands.Include(b => b.Bikes).ToListAsync();
            return data;
        }

        public async Task<Brand>GetBrandById(Guid id)
        {
            var data = await _context.Brands.Where(b => b.Id == id).SingleOrDefaultAsync();
            return data;
        }
        public async Task<List<Brand>> Search(string searchText)
        {
            var data = await _context.Brands.Where(b => b.Name.Contains(searchText)).Take(5).ToListAsync();
            return data;
        }
    }
}
