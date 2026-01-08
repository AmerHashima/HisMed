using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface ISystemUserRepository : IRepository<SystemUser>
{
    Task<SystemUser?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<SystemUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> IsUsernameUniqueAsync(string username, Guid? excludeUserId = null, CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(string email, Guid? excludeUserId = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<SystemUser>> GetActiveUsersAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<SystemUser>> GetUsersByRoleAsync(int roleId, CancellationToken cancellationToken = default);
}