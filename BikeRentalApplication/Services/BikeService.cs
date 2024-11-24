using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.DTOs.ResponseDTOs;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using BikeRentalApplication.IServices;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace BikeRentalApplication.Services
{
    public class BikeService : IBikeService
    {
        private readonly IBikeRepository _bikeRepository;

        public BikeService(IBikeRepository bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }

        public async Task<List<BikeResponse>> GetBike(string? type, Guid? brandId)
        {
            var data = new List<Bike>();
            if (brandId == null && type == null)
            {
                data = await _bikeRepository.GetBike();
            }
            else if (brandId != null && type == null)
            {
                data = await _bikeRepository.GetBikeBrandFilter(brandId);
            }
            else if (brandId == null && type != null)
            {
                data = await _bikeRepository.GetBikeTypeFilter(type);
            }
            else
            {
                data = await _bikeRepository.GetBikeFilter(type, brandId);
            }
            var bikes = data.Select(b => new BikeResponse
            {
                Id = b.Id,
                BrandId = b.BrandId,
                Model = b.Model,
                Type = b.Type,
                RatePerHour = b.RatePerHour,
                Description = b.Description,
                Brand = new BrandResponse
                {
                    Name = b.Brand.Name,
                    Id = b.Brand.Id,
                },
                Images = b.Images.Select(i => new ImageResponse
                {
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    BikeId = i.BikeId,

                }).ToList() ?? [],
                InventoryUnits = b.InventoryUnits.Select(u => new InventoryUnitResponse
                {
                    RegistrationNo = u.RegistrationNo,
                    YearOfManufacture = u.YearOfManufacture,
                    Availability = u.Availability,
                    IsDeleted = u.IsDeleted,
                    RentalRecords = u.RentalRecords,
                    BikeId = u.BikeId,
                }).ToList() ?? [],
            }).ToList();
            return bikes;
        }

        public async Task<BikeResponse> GetBike(Guid id)
        {
            var data = await _bikeRepository.GetBike(id);
            var response = new BikeResponse
            {
                Id = data.Id,
                BrandId = data.BrandId,
                Model = data.Model,
                Type = data.Type,
                RatePerHour = data.RatePerHour,
                Description = data.Description,
                Brand = new BrandResponse
                {
                    Name = data.Brand.Name,
                    Id = data.Brand.Id,
                },
                Images = data.Images.Select(i => new ImageResponse
                {
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    BikeId = i.BikeId,

                }).ToList() ?? [],
                InventoryUnits = data.InventoryUnits.Select(u => new InventoryUnitResponse
                {
                    RegistrationNo = u.RegistrationNo,
                    YearOfManufacture = u.YearOfManufacture,
                    Availability = u.Availability,
                    IsDeleted = u.IsDeleted,
                    RentalRecords = u.RentalRecords,
                    BikeId = u.BikeId,
                }).ToList() ?? [],
            };
            // return await _bikeRepository.GetBike(id);
            return response;
        }

        public async Task<Bike> PutBike(BikePutRequest bikePutRequest, Guid id)
        {
            var getBike = await _bikeRepository.GetBike(id);
            if (getBike != null)
            {
                getBike.BrandId = bikePutRequest.BrandId;
                getBike.RatePerHour = bikePutRequest.RatePerHour;
                getBike.Model = bikePutRequest.Model;
                getBike.Type = bikePutRequest.Type;
                return await _bikeRepository.PutBike(getBike);
            }
            else
            {
                throw new Exception();
            }

        }
        public async Task<Bike> PostBike(BikeRequest bikeRequest)
        {
            var bike = new Bike
            {
                BrandId = bikeRequest.BrandId,
                Model = bikeRequest.Model,
                Type = bikeRequest.Type,
                RatePerHour = bikeRequest.RatePerHour,
                Description = bikeRequest.Description,
                Images = bikeRequest.Images?.Select(i => new Image
                {
                    ImagePath = i.ImagePath
                }).ToList()
            };

            return await _bikeRepository.PostBike(bike);
        }
        public async Task<string> DeleteBike(Guid id)
        {
            return await _bikeRepository.DeleteBike(id);
        }
    }
}
