using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IServices
{
    public interface IBikeService
    {
        Task<List<Bike>> GetBike();
        Task<Bike> GetBike(Guid id);
        Task<Bike> PutBike(Bike bike, Guid id);
        Task<Bike> PostBike(Bike bike);
        Task<string> DeleteBike(Guid id);
    }
}
