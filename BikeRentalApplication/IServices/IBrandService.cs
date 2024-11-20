using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IServices
{
    public interface IBrandService
    {
        Task<Brand> AddBrand(BrandRequest brandRequest);
        Task<List<Brand>> GetAllBrands();
    }
}
