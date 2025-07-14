using EhsaasHub.Models.ERP.HR;

namespace EhsaasHub.Repositories.HR
{
    public interface IStaffMemberRepository
    {
        Task<IEnumerable<StaffMember>> GetAllAsync();
        Task<StaffMember?> GetByIdAsync(int id);
        Task AddAsync(StaffMember staff);
        Task UpdateAsync(StaffMember staff);
        Task DeleteAsync(int id);
    }
}
