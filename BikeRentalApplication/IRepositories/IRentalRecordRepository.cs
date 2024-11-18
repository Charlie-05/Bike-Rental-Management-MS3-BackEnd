using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IRepositories
{
    public interface IRentalRecordRepository
    {
        Task<RentalRecord> PostRentalRecord(RentalRecord RentalRecord);
        Task<List<RentalRecord>> GetRentalRecords();
        Task<RentalRecord> GetRentalRecord(Guid id);
        Task<RentalRecord> UpdateRentalRecord(RentalRecord RentalRecord);
        Task<string> DeleteRentalRecord(Guid id);
    }
}
