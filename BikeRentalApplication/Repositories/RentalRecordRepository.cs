using BikeRentalApplication.Database;
using BikeRentalApplication.Entities;
using BikeRentalApplication.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApplication.Repositories
{
    public class RentalRecordRepository : IRentalRecordRepository
    {
        private readonly RentalDbContext _dbContext;

        public RentalRecordRepository(RentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<RentalRecord> PostRentalRecord(RentalRecord RentalRecord)
        {
            var data = await _dbContext.RentalRecords.AddAsync(RentalRecord);
            await _dbContext.SaveChangesAsync();

            return data.Entity;
        }
        public async Task<List<RentalRecord>> GetRentalRecords()
        {
            var data = await _dbContext.RentalRecords
                        .Where(r => r.RentalReturn != null)
                        .OrderByDescending(r => r.RentalReturn)
                        .Include(r => r.RentalRequest)
                        .ToListAsync();
            return data;
        }
        public async Task<List<RentalRecord>> GetIncompleteRentalRecords()
        {
            var data = await _dbContext.RentalRecords.Where(r => r.RentalReturn == null).OrderByDescending(r => r.RentalOut).Include(r => r.RentalRequest).ToListAsync();
            return data;
        }

        public async Task<RentalRecord> GetRentalRecord(Guid id)
        {
            var request = await _dbContext.RentalRecords.Include(r => r.RentalRequest).FirstOrDefaultAsync(x => x.Id == id);
            if (request == null)
            {
                throw new Exception();
            }

            return request;
        }

        public async Task<RentalRecord> GetRentalRecordbyRequestID(Guid RequestId)
        {
            var data = await _dbContext.RentalRecords.Where(r => r.RentalRequestId == RequestId).SingleOrDefaultAsync();
            return data;
        }

        public async Task<RentalRecord> UpdateRentalRecord(RentalRecord RentalRecord)
        {
            var data = _dbContext.RentalRecords.Update(RentalRecord);
            await _dbContext.SaveChangesAsync();

            return data.Entity;
        }

        public async Task<string> DeleteRentalRecord(Guid id)
        {
            var record = await _dbContext.RentalRecords.FindAsync(id);
            if (record == null)
            {
                throw new ArgumentException();
            }

            _dbContext.RentalRecords.Remove(record);
            await _dbContext.SaveChangesAsync();

            return "Successfully Deleted...";
        }
        public async Task<List<RentalRecord>>GetRecordsByRange(DateTime Start , DateTime End)
        {
            var data = await _dbContext.RentalRecords.Where(r => r.RentalReturn > Start &&  r.RentalReturn < End && r.RentalReturn!= null).Include(r => r.RentalRequest).ToListAsync();
            return data;
        }

        public async Task<List<RentalRecord>> Search(string searchText)
        {
            var data = await _dbContext.RentalRecords.Where(b => b.BikeRegNo.Contains(searchText) || b.RentalReturn.ToString().Contains(searchText) ||
            b.RentalOut.ToString().Contains(searchText)).Take(5).ToListAsync();
            return data;
        }
    }
}
