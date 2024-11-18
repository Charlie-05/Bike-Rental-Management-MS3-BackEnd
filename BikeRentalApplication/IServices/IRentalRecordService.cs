using BikeRentalApplication.DTOs.RequestDTOs;
using BikeRentalApplication.Entities;

namespace BikeRentalApplication.IServices
{
    public interface IRentalRecordService
    {
        Task<List<RentalRecord>> GetRentalRecords();
        Task<RentalRecord> GetRentalRecord(Guid id);
        Task<RentalRecord> UpdateRentalRecord(Guid id ,RentalRecord RentalRecord);
        Task<RentalRecord> PostRentalRecord(RentalRecRequest rentalRecRequest);
        Task<string> DeleteRentalRecord(Guid id);
    }
}
