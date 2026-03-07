using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface IDoctorBranchRepository : IBaseRepository<DoctorBranch>
{
    Task<IEnumerable<DoctorBranch>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
}
