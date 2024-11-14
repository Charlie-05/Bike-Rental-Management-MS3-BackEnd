using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.DTOs.ResponseDTOs;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using BikeRentalApplication.IServices;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApplication.Services
{
    public class BikeService : IBikeService
    {
        private readonly IBikeRepository _bikeRepository;

        public BikeService(IBikeRepository bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }

        public async Task<List<BikeResponse>> GetBike()
        {
            var data = await _bikeRepository.GetBike();
            var bikes = data.Select(b => new BikeResponse
            {
                Id = b.Id,
                Brand = b.Brand,
                Model = b.Model,
                Type = b.Type,
                RatePerHour = b.RatePerHour,
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

        public async Task<Bike> GetBike(Guid id)
        {
            return await _bikeRepository.GetBike(id);
        }

        public async Task<Bike> PutBike(BikeRequest bikeRequest, Guid id)
        {
            var getBike = await _bikeRepository.GetBike(id);
            if (getBike != null)
            {
                return await _bikeRepository.PutBike(getBike);
            }
            else
            {
                throw new Exception();
            }

        }
        public async Task<Bike> PostBike(Bike bike)
        {
            return await _bikeRepository.PostBike(bike);
        }
        public async Task<string> DeleteBike(Guid id)
        {
            return await _bikeRepository.DeleteBike(id);
        }
    }
}
