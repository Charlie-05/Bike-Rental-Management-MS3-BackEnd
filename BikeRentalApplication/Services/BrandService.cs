using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.DTOs.ResponseDTOs;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using BikeRentalApplication.IServices;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApplication.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<Brand> AddBrand(BrandRequest brandRequest)
        {
            var brand = new Brand
            {
                Name = brandRequest.Name,
            };
            var data = await _brandRepository.AddBrand(brand);
            return data;
        }
       public async Task<List<Brand>> GetAllBrands()
        {
            var data = await _brandRepository.GetAllBrands();
            return data;
        }
        public async Task<BrandResponse> GetBrandById(Guid id)
        {
            var data = await _brandRepository.GetBrandById(id);
            var brandResponse = new BrandResponse
            {
                Name = data.Name,
                Id = id
            };
            return brandResponse;
        }
    }
}
