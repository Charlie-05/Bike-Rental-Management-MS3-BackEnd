using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.DTOs.ResponseDTOs;
using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IServices
{
    public interface IBrandService
    {
        Task<Brand> AddBrand(BrandRequest brandRequest);
        Task<List<Brand>> GetAllBrands();
        Task<BrandResponse> GetBrandById(Guid id);
    }
}
