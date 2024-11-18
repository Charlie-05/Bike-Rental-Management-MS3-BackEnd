using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using BikeRentalApplication.IServices;

namespace BikeRentalApplication.Services
{
    public class RentalRequestService : IRentalRequestService
    {
        private readonly IRentalRequestRepository _rentalRequestRepository;

        public RentalRequestService(IRentalRequestRepository rentalRequestRepository)
        {

            _rentalRequestRepository = rentalRequestRepository;
        }

        public async Task<List<RentalRequest>> GetRentalRequests(Status? status)
        {
            if (status == null)
            {
                return await _rentalRequestRepository.GetRentalRequests();
            }
            else
            {
                return await _rentalRequestRepository.GetRentalRequestsByStatus(status);
            }

        }

        public async Task<RentalRequest> GetRentalRequest(Guid id)
        {
            var data = await _rentalRequestRepository.GetRentalRequest(id);
            return data;
        }

        public async Task<RentalRequest> UpdateRentalRequest(Guid id ,RentalRequest rentalRequest)
        {
            var getRequest = await _rentalRequestRepository.GetRentalRequest(id);
            if (getRequest != null) {
                var data = await _rentalRequestRepository.UpdateRentalRequest(rentalRequest);
                return data;
            }
            else
            {
                throw new NotImplementedException();
            }
            
        }

        public async Task<RentalRequest> AcceptRentalRequest(Guid Id)
        {
            var request = await _rentalRequestRepository.GetRentalRequest(Id);
            request.Status = Status.Accepted;
            var data = await _rentalRequestRepository.UpdateRentalRequest(request);
            return data;
        }
        public async Task<RentalRequest> DeclineRentalRequest(Guid Id)
        {
            var request = await _rentalRequestRepository.GetRentalRequest(Id);
            request.Status = Status.Declined;
            var data = await _rentalRequestRepository.UpdateRentalRequest(request);
            return data;
        }

        public async Task<RentalRequest> PostRentalRequest(RentalReqRequest rentalReqRequest)
        {
            var rentalRequest = new RentalRequest()
            {
                BikeId = rentalReqRequest.BikeId,
                RequestTime = rentalReqRequest.RequestTime,
                NICNumber = rentalReqRequest.NICNumber,
                Status = Status.Pending,
            };
            var data = await _rentalRequestRepository.PostRentalRequest(rentalRequest);
            return data;
        }

        public async Task<string> DeleteInventoryUnit(Guid id)
        {
            var data = await _rentalRequestRepository.DeleteRentalRequest(id);
            return data;
        }
    }
}
