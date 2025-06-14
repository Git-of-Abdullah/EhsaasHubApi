using EhsaasHub.Models.ERP.HR;

namespace EhsaasHub.Repositories.HR
{
    public interface ISalaryRecordRepository
    {
        Task<IEnumerable<SalaryRecord>> GetAllAsync();
        Task<SalaryRecord?> GetByIdAsync(int id);
        Task<IEnumerable<SalaryRecord>> GetByStaffMemberIdAsync(int staffId);
        Task AddAsync(SalaryRecord record);
        Task UpdateAsync(SalaryRecord record);
        Task DeleteAsync(int id);
    }
}
