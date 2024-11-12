using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IServices
{
    public interface IRentalRequestService
    {
        Task<List<RentalRequest>> GetRentalRequests();
        Task<RentalRequest> GetRentalRequest(Guid id);
        Task<RentalRequest> UpdateRentalRequest(RentalRequest rentalRequest);
        Task<RentalRequest> PostRentalRequest(RentalRequest rentalRequest);
        Task<string> DeleteInventoryUnit(Guid id);
    }
}
