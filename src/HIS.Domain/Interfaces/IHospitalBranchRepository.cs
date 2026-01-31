using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface IHospitalBranchRepository : IBaseRepository<HospitalBranch>
{
    Task<HospitalBranch?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<HospitalBranch>> GetActiveBranchesAsync(CancellationToken cancellationToken = default);
    Task<bool> BranchCodeExistsAsync(string code, Guid? excludeId = null, CancellationToken cancellationToken = default);
}