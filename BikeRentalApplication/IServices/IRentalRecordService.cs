using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.DTOs.ResponseDTOs;
using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IServices
{
    public interface IRentalRecordService
    {
        Task<List<RentalRecord>> GetRentalRecords(State? state);
        Task<RentalRecord> GetRentalRecord(Guid id);
        Task<RentalRecord> UpdateRentalRecord(Guid id, RentalRecord RentalRecord);
        Task<RentalRecord> CompleteRentalRecord(Guid id, RentalRecPutRequest rentalRecPutRequest);
        Task<List<RentalRecord>> GetOverDueRentalsOfUser(string? nicNo);
        Task<RentalRecord> PostRentalRecord(RentalRecRequest rentalRecRequest);
        Task<string> DeleteRentalRecord(Guid id);
        Task<PaymentResponse> GetPayment(Guid id);
        Task<decimal> PostReview(RatingRequest ratingRequest);
        Task<List<RentalRecord>> GetRecordsByRange(DateTime Start, DateTime End);
        Task<Dictionary<string, string>> Search(string searchText);
    }
}
