using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HIS.Infrastructure.Repositories;

public class SystemUserRepository : BaseRepository<SystemUser>, ISystemUserRepository
{
    public SystemUserRepository(HISDbContext context) : base(context)
    {
    }

    public async Task<SystemUser?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _context.SystemUsers
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
    }

    public async Task<SystemUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.SystemUsers
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<bool> IsUsernameUniqueAsync(string username, Guid? excludeUserId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.SystemUsers.Where(x => !x.IsDeleted && x.Username == username);

        if (excludeUserId.HasValue)
        {
            query = query.Where(x => x.Oid != excludeUserId.Value);
        }

        return !await query.AnyAsync(cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeUserId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.SystemUsers.Where(x => !x.IsDeleted && x.Email == email);

        if (excludeUserId.HasValue)
        {
            query = query.Where(x => x.Oid != excludeUserId.Value);
        }

        return !await query.AnyAsync(cancellationToken);
    }

    public async Task<IEnumerable<SystemUser>> GetActiveUsersAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SystemUsers
            .Where(x => !x.IsDeleted && x.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<SystemUser>> GetUsersByRoleAsync(int roleId, CancellationToken cancellationToken = default)
    {
        return await _context.SystemUsers
            .Where(x => !x.IsDeleted && x.RoleID == roleId)
            .ToListAsync(cancellationToken);
    }
}