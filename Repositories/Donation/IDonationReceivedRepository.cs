using EhsaasHub.Models.ERP;

namespace EhsaasHub.Repositories.Donation
{
    public interface IDonationReceivedRepository
    {
        Task<IEnumerable<DonationReceived>> GetAllAsync();
        Task<DonationReceived?> GetByIdAsync(int id);
        Task AddAsync(DonationReceived donation);
        Task UpdateAsync(DonationReceived donation);
        Task DeleteAsync(int id);
    }
}
