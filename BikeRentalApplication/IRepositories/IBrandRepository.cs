using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IRepositories
{
    public interface IBrandRepository 
    {
        Task<Brand> AddBrand(Brand brand);
       Task<List<Brand>> GetAllBrands();
        Task<Brand> GetBrandById(Guid id);
        Task<List<Brand>> Search(string searchText);
    }
}
