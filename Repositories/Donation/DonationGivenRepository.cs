using EhsaasHub.Data;
using EhsaasHub.Models.ERP;
using Microsoft.EntityFrameworkCore;

namespace EhsaasHub.Repositories.Donation
{
    public class DonationGivenRepository : IDonationGivenRepository
    {
        private readonly ApplicationDbContext _context;

        public DonationGivenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DonationGiven>> GetAllAsync()
        {
            return await _context.DonationsGiven.ToListAsync();
        }

        public async Task<DonationGiven?> GetByIdAsync(int id)
        {
            return await _context.DonationsGiven.FindAsync(id);
        }

        public async Task AddAsync(DonationGiven donation)
        {
            await _context.DonationsGiven.AddAsync(donation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DonationGiven donation)
        {
            _context.DonationsGiven.Update(donation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var donation = await _context.DonationsGiven.FindAsync(id);
            if (donation != null)
            {
                _context.DonationsGiven.Remove(donation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
