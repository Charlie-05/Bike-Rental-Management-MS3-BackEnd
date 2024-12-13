using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IRepositories
{
    public interface IRentalRequestRepository
    {
        Task<RentalRequest> PostRentalRequest(RentalRequest rentalRequest);
        Task<List<RentalRequest>> GetRentalRequests();
        Task<List<RentalRequest>> GetRentalRequestsByStatus(Status? status);
        Task<RentalRequest> GetRentalRequest(Guid id);
        Task<RentalRequest> UpdateRentalRequest(RentalRequest rentalRequest);
        Task<string> DeleteRentalRequest(Guid id);
        Task<List<RentalRequest>> Search(string searchText);
    }
}
