using EhsaasHub.Data;
using EhsaasHub.Models.ERP.HR;
using Microsoft.EntityFrameworkCore;

namespace EhsaasHub.Repositories.HR
{
    public class SalaryRecordRepository : ISalaryRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public SalaryRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalaryRecord>> GetAllAsync()
        {
            return await _context.SalaryRecords
                .Include(s => s.StaffMember)
                .ToListAsync();
        }

        public async Task<SalaryRecord?> GetByIdAsync(int id)
        {
            return await _context.SalaryRecords
                .Include(s => s.StaffMember)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<SalaryRecord>> GetByStaffMemberIdAsync(int staffId)
        {
            return await _context.SalaryRecords
                .Where(s => s.StaffMemberId == staffId)
                .Include(s => s.StaffMember)
                .ToListAsync();
        }

        public async Task AddAsync(SalaryRecord record)
        {
            await _context.SalaryRecords.AddAsync(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SalaryRecord record)
        {
            _context.SalaryRecords.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _context.SalaryRecords.FindAsync(id);
            if (record != null)
            {
                _context.SalaryRecords.Remove(record);
                await _context.SaveChangesAsync();
            }
        }
    }
}
