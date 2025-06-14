using EhsaasHub.Models.ERP;

namespace EhsaasHub.Repositories.Donation
{
    public interface IDonationGivenRepository
    {
        Task<IEnumerable<DonationGiven>> GetAllAsync();
        Task<DonationGiven?> GetByIdAsync(int id);
        Task AddAsync(DonationGiven donation);
        Task UpdateAsync(DonationGiven donation);
        Task DeleteAsync(int id);
    }
}
