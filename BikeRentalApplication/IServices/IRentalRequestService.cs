using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IServices
{
    public interface IRentalRequestService
    {
        Task<List<RentalRequest>> GetRentalRequests();
        Task<RentalRequest> GetRentalRequest(Guid id);
        Task<RentalRequest> UpdateRentalRequest(RentalRequest rentalRequest);
        Task<RentalRequest> PostRentalRequest(RentalReqRequest rentalReqRequest);
        Task<RentalRequest> AcceptRentalRequest(Guid Id);
        Task<RentalRequest> DeclineRentalRequest(Guid Id);
        Task<string> DeleteInventoryUnit(Guid id);
    }
}
