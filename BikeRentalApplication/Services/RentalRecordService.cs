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
        private readonly IBikeRepository _bikeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IEmailService _emailService;

        public RentalRecordService(IRentalRecordRepository rentalRecordRepository, IRentalRequestRepository rentalRequestRepository, IInventoryUnitRepository inventoryUnitRepository ,
            IBikeRepository bikeRepository , IEmailService emailService, IUserRepository userRepository, IBrandRepository brandRepository)
        {
            _rentalRecordRepository = rentalRecordRepository;
            _rentalRequestRepository = rentalRequestRepository;
            _inventoryUnitRepository = inventoryUnitRepository;
            _bikeRepository = bikeRepository;
            _userRepository = userRepository;
            _brandRepository = brandRepository;
            _emailService = emailService;
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
                var getRequest = await _rentalRequestRepository.GetRentalRequest(getRecord.RentalRequestId);
                var mailReq = new MailRequest();
                mailReq.Bike = getUnit.Bike;
                mailReq.User = getRequest.User; 
                mailReq.RentalRecord = getRecord;
                mailReq.Template = EmailTemplate.Payment;
                await _emailService.SendEmail(mailReq);
                return data;
            }
            else
            {
                throw new NotImplementedException("Try Again");
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
        public async Task<decimal> PostReview(RatingRequest ratingRequest)
        {
            var getRecord = await _rentalRecordRepository.GetRentalRecord(ratingRequest.RecordId);

            if (ratingRequest.Rating != null)
            {
                var getBike = await _bikeRepository.GetBike(getRecord.RentalRequest.BikeId);
                getBike.NumberOfRatings = getBike.NumberOfRatings + 1;
                getBike.Rating = (getBike.Rating + (decimal)ratingRequest.Rating) / getBike.NumberOfRatings;
                var updated = await _bikeRepository.PutBike(getBike);
            }
            if (ratingRequest.Review != null)
            {

                getRecord.Review = ratingRequest.Review;
                var updatedRecord = await _rentalRecordRepository.UpdateRentalRecord(getRecord);
            }
            return (decimal)ratingRequest.Rating;
        }

        public async Task<string> DeleteRentalRecord(Guid id)
        {
            var data = await _rentalRecordRepository.DeleteRentalRecord(id);
            return data;
        }
        public async Task<List<RentalRecord>> GetRecordsByRange(DateTime Start, DateTime End)
        {
            var data = await _rentalRecordRepository.GetRecordsByRange(Start, End);
            return data;
        }

        public async Task<Dictionary<string, string>> Search(string searchText)
        {
            var bikes = await _bikeRepository.Search(searchText);
            var users = await _userRepository.Search(searchText);
            var brands = await _brandRepository.Search(searchText);
            var inventoryUnits = await _inventoryUnitRepository.Search(searchText);
            var rentalRecords = await _rentalRecordRepository.Search(searchText);
            var rentalRequests = await _rentalRequestRepository.Search(searchText);
            Dictionary<string, string> res  = new Dictionary<string, string>();
            foreach (var bike in bikes)
            {
                res.Add(bike.Id.ToString(), "bike");
            }
            foreach (var user in users)
            {
                res.Add(user.NICNumber, "user");
            }
            foreach (var unit in inventoryUnits)
            {
                res.Add(unit.RegistrationNo, "unit");
            }
            foreach (var brand in brands)
            {
                res.Add(brand.Id.ToString(), "brand");
            }
            foreach (var record in rentalRecords)
            {
                res.Add(record.Id.ToString(), "record");
            }
            foreach (var request in rentalRequests)
            {
                res.Add(request.Id.ToString(), "request");
            }
            return res;
        }
    }
}
