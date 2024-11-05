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

        public async Task<List<Bike>> GetBike()
        {
            return await _bikeRepository.GetBike();
        }

        public async Task<Bike> GetBike(Guid id)
        {
            return await _bikeRepository.GetBike(id);
        }

        public async Task<Bike> PutBike(Bike bike , Guid id)
        {
            return await _bikeRepository.PutBike(bike);
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
