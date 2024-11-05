using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IRepositories
{
    public interface IBikeRepository 
    {
        Task<List<Bike>> GetBike();
        Task<Bike> GetBike(Guid id);
        Task<Bike> PutBike(Bike bike);
        Task<Bike> PostBike(Bike bike);
        Task<string> DeleteBike(Guid id);
    }
}
