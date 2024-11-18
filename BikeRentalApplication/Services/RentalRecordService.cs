using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using BikeRentalApplication.IServices;

namespace BikeRentalApplication.Services
{
    public class RentalRecordService : IRentalRecordService
    {
        private readonly IRentalRecordRepository _rentalRecordRepository;
        private readonly IRentalRequestRepository _rentalRequestRepository;

        public RentalRecordService(IRentalRecordRepository rentalRecordRepository , IRentalRequestRepository rentalRequestRepository )
        {
            _rentalRecordRepository = rentalRecordRepository;
            _rentalRequestRepository = rentalRequestRepository;
        }

        public async Task<List<RentalRecord>> GetRentalRecords()
        {
            var data = await _rentalRecordRepository.GetRentalRecords();
            return data;
        }

        public async Task<RentalRecord> GetRentalRecord(Guid id)
        {
            var data = await _rentalRecordRepository.GetRentalRecord(id);
            var getRequest = await _rentalRequestRepository.GetRentalRequest(data.RentalRequestId);
            var getRate = getRequest.Bike.RatePerHour;
            return data;
        }

        public async Task<decimal> GetPayment(Guid id)
        {
            var data = await _rentalRecordRepository.GetRentalRecord(id);
            var getRequest = await _rentalRequestRepository.GetRentalRequest(data.RentalRequestId);
            var getRate = getRequest.Bike.RatePerHour;
            return getRate;
        }

        public async Task<RentalRecord> UpdateRentalRecord(Guid id, RentalRecord RentalRecord)
        {
            var getRecord = await _rentalRecordRepository.GetRentalRecord(id);
            if (getRecord != null)
            {
                var data = await _rentalRecordRepository.UpdateRentalRecord(RentalRecord);
                return data;
            }
            else
            {
                throw new NotImplementedException();
            }

        }

        public async Task<RentalRecord> PostRentalRecord(RentalRecRequest rentalRecRequest)
        {
            var RentalRecord = new RentalRecord()
            {
               RentalRequestId = rentalRecRequest.RentalRequestId,
               RentalOut = DateTime.Now,
               BikeRegNo = rentalRecRequest.BikeRegNo,
            };
            var data = await _rentalRecordRepository.PostRentalRecord(RentalRecord);
            return data;
        }

        public async Task<string> DeleteRentalRecord(Guid id)
        {
            var data = await _rentalRecordRepository.DeleteRentalRecord(id);
            return data;
        }
    }
}
