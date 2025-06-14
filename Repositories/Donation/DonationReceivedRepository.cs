using EhsaasHub.Data;
using EhsaasHub.Models.ERP;
using Microsoft.EntityFrameworkCore;

namespace EhsaasHub.Repositories.Donation
{
    public class DonationReceivedRepository : IDonationReceivedRepository 
    {
        private readonly ApplicationDbContext _context;

        public DonationReceivedRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DonationReceived>> GetAllAsync()
        {
            return await _context.DonationsReceived.ToListAsync();
        }

        public async Task<DonationReceived?> GetByIdAsync(int id)
        {
            return await _context.DonationsReceived.FindAsync(id);
        }

        public async Task AddAsync(DonationReceived donation)
        {
            await _context.DonationsReceived.AddAsync(donation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DonationReceived donation)
        {
            _context.DonationsReceived.Update(donation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var donation = await _context.DonationsReceived.FindAsync(id);
            if (donation != null)
            {
                _context.DonationsReceived.Remove(donation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
