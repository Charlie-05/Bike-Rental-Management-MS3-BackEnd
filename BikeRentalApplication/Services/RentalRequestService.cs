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

        public async Task<List<RentalRequest>> GetInventoryUnits()
        {
            return await _rentalRequestRepository.GetRentalRequests();
        }

        public async Task<RentalRequest> GetInventoryUnit(Guid id)
        {
            var data = await _rentalRequestRepository.GetRequest(id);


            return data;
        }

        public async Task<RentalRequest> PutInventoryUnit(RentalRequest rentalRequest)
        {
            var data = await _rentalRequestRepository.UpdateRequest(rentalRequest);
            return rentalRequest;
        }

        //public async Task<RentalRequest> PostInventoryUnit(InventoryUnit inventoryUnit)
        //{
        //    var data = await _rentalRequestRepository.PostInventoryUnit(inventoryUnit);
        //    return inventoryUnit;
        //}

        //public async Task<string> DeleteInventoryUnit(Guid id)
        //{
        //    var data = await _invenroryUnitRepository.DeleteInventoryUnit(id);
        //    return data;
        //}
    }
}
