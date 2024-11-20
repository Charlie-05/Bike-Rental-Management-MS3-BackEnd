using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IRepositories
{
    public interface IBrandRepository 
    {
        Task<Brand> AddBrand(Brand brand);
       Task<List<Brand>> GetAllBrands();
    }
}
