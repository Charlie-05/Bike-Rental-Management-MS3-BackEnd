﻿using BikeRentalApplication.Entities;
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

        public async Task<List<RentalRequest>> GetRentalRequests()
        {
            return await _rentalRequestRepository.GetRentalRequests();
        }

        public async Task<RentalRequest> GetRentalRequest(Guid id)
        {
            var data = await _rentalRequestRepository.GetRentalRequest(id);
            return data;
        }

        public async Task<RentalRequest> UpdateRentalRequest(RentalRequest rentalRequest)
        {
            var data = await _rentalRequestRepository.UpdateRentalRequest(rentalRequest);
            return data;
        }

        public async Task<RentalRequest> PostRentalRequest(RentalRequest rentalRequest)
        {
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