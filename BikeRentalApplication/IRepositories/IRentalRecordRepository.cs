using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IRepositories
{
    public interface IRentalRecordRepository
    {
        Task<RentalRecord> PostRentalRecord(RentalRecord RentalRecord);
        Task<List<RentalRecord>> GetRentalRecords();
        Task<List<RentalRecord>> GetIncompleteRentalRecords();
        Task<RentalRecord> GetRentalRecord(Guid id);
        Task<RentalRecord> GetRentalRecordbyRequestID(Guid RequestId);
        Task<RentalRecord> UpdateRentalRecord(RentalRecord RentalRecord);
        Task<string> DeleteRentalRecord(Guid id);
        Task<List<RentalRecord>> GetRecordsByRange(DateTime Start, DateTime End);
        Task<List<RentalRecord>> Search(string searchText);
    }
}
