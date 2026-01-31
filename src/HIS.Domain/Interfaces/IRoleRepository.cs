using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> RoleNameExistsAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);
}