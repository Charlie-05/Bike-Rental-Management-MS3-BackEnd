using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IRepositories
{
    public interface IRentalRequestRepository
    {
        Task<RentalRequest> PostRentalRequest(RentalRequest rentalRequest);
        Task<List<RentalRequest>> GetRentalRequests();
        Task<RentalRequest> GetRequest(Guid id);
        Task<RentalRequest> UpdateRequest(RentalRequest rentalRequest);
        Task<string> DeleteRequest(Guid id);

    }
}
