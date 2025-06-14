using EhsaasHub.Data;
using EhsaasHub.Models.ERP.HR;
using Microsoft.EntityFrameworkCore;

namespace EhsaasHub.Repositories.HR
{
    public class StaffMemberRepository : IStaffMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public StaffMemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StaffMember>> GetAllAsync()
        {
            return await _context.StaffMembers.ToListAsync();
        }

        public async Task<StaffMember?> GetByIdAsync(int id)
        {
            return await _context.StaffMembers.FindAsync(id);
        }

        public async Task AddAsync(StaffMember staff)
        {
            await _context.StaffMembers.AddAsync(staff);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StaffMember staff)
        {
            _context.StaffMembers.Update(staff);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var staff = await _context.StaffMembers.FindAsync(id);
            if (staff != null)
            {
                _context.StaffMembers.Remove(staff);
                await _context.SaveChangesAsync();
            }
        }
    }
}
