using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.DTOs.ResponseDTOs;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using BikeRentalApplication.IServices;

namespace BikeRentalApplication.Services
{
    public class RentalRecordService : IRentalRecordService
    {
        private readonly IRentalRecordRepository _rentalRecordRepository;
        private readonly IRentalRequestRepository _rentalRequestRepository;
        private readonly IInventoryUnitRepository _inventoryUnitRepository;

        public RentalRecordService(IRentalRecordRepository rentalRecordRepository, IRentalRequestRepository rentalRequestRepository, IInventoryUnitRepository inventoryUnitRepository)
        {
            _rentalRecordRepository = rentalRecordRepository;
            _rentalRequestRepository = rentalRequestRepository;
            _inventoryUnitRepository = inventoryUnitRepository;
        }

        public async Task<List<RentalRecord>> GetRentalRecords(State? state)
        {
            if (state == State.Incompleted)
            {
                var data = await _rentalRecordRepository.GetIncompleteRentalRecords();
                return data;
            }
            else if (state == State.Completed)
            {
                var data = await _rentalRecordRepository.GetRentalRecords();
                return data;
            }
            else
            {
                throw new Exception("Invalid State Code");
            }

        }

        public async Task<List<RentalRecord>> GetOverDueRentalsOfUser(string? nicNo)
        {
            var data = await _rentalRecordRepository.GetIncompleteRentalRecords();
            var overdue = new List<RentalRecord>();
            var now = DateTime.Now;
            if (nicNo != null)
            {
                foreach (RentalRecord record in data)
                {
                    if (now.Subtract((DateTime)record.RentalOut).Minutes > 5 && record.RentalRequest.UserId == nicNo)
                    {
                        overdue.Add(record);
                    }

                }
            }
            else
            {
                foreach (RentalRecord record in data)
                {
                    if (now.Subtract((DateTime)record.RentalOut).Minutes > 5)
                    {
                        overdue.Add(record);
                    }

                }
            }
        
            return overdue;
        }



        public async Task<RentalRecord> GetRentalRecord(Guid id)
        {
            var data = await _rentalRecordRepository.GetRentalRecord(id);
            return data;
        }

        public async Task<PaymentResponse> GetPayment(Guid id)
        {
            var data = await _rentalRecordRepository.GetRentalRecord(id);
            var getRequest = await _rentalRequestRepository.GetRentalRequest(data.RentalRequestId);
            var getRate = getRequest.Bike.RatePerHour;
            var timeSpan = DateTime.Now.Subtract((DateTime)data.RentalOut);
            var payment = getRate * (Decimal)timeSpan.TotalHours;

            var paymentResponse = new PaymentResponse
            {
                Payment = payment,
                RatePerHour = getRate,
            };
            return paymentResponse;
        }

        public async Task<RentalRecord> UpdateRentalRecord(Guid id, RentalRecord rentalRecord)
        {
            var getRecord = await _rentalRecordRepository.GetRentalRecord(id);
            if (getRecord != null)
            {
                var data = await _rentalRecordRepository.UpdateRentalRecord(rentalRecord);
                return data;
            }
            else
            {
                throw new NotImplementedException();
            }

        }

        public async Task<RentalRecord> CompleteRentalRecord(Guid id, RentalRecPutRequest rentalRecPutRequest)
        {
            var getRecord = await _rentalRecordRepository.GetRentalRecord(id);
            if (getRecord != null)
            {
                getRecord.RentalReturn = DateTime.Now;
                getRecord.Payment = rentalRecPutRequest.Payment;
               
                var data = await _rentalRecordRepository.UpdateRentalRecord(getRecord);
                var getUnit = await _inventoryUnitRepository.GetInventoryUnit(getRecord.BikeRegNo);
                getUnit.Availability = true;
               await _inventoryUnitRepository.PutInventoryUnit(getUnit);
                return data;
            }
            else
            {
                throw new NotImplementedException();
            }

        }

        public async Task<RentalRecord> PostRentalRecord(RentalRecRequest rentalRecRequest)
        {
            var getRequest = await _rentalRequestRepository.GetRentalRequest(rentalRecRequest.RentalRequestId);
            var RentalRecord = new RentalRecord()
            {
                RentalRequestId = rentalRecRequest.RentalRequestId,
                RentalOut = DateTime.Now,
                BikeRegNo = rentalRecRequest.BikeRegNo,

            };
            if(getRequest.User.IsVerified == false)
            {
                throw new Exception("Unverified user.");
            }
          
            var data = await _rentalRecordRepository.PostRentalRecord(RentalRecord);
            var getUnit = await _inventoryUnitRepository.GetInventoryUnit(rentalRecRequest.BikeRegNo);
            getUnit.Availability = false;
            var updatedUnit = await _inventoryUnitRepository.PutInventoryUnit(getUnit);
            getRequest.Status = Status.OnRent;
            var updated = await _rentalRequestRepository.UpdateRentalRequest(getRequest);
            return data;
        }

        public async Task<string> DeleteRentalRecord(Guid id)
        {
            var data = await _rentalRecordRepository.DeleteRentalRecord(id);
            return data;
        }
    }
}
