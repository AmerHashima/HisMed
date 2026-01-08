using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HIS.Infrastructure.Repositories;

public class SystemUserRepository : ISystemUserRepository
{
    private readonly HISDbContext _context;

    public SystemUserRepository(HISDbContext context)
    {
        _context = context;
    }

    public async Task<SystemUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.SystemUsers
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Oid == id, cancellationToken);
    }

    public async Task<IEnumerable<SystemUser>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SystemUsers
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<SystemUser>> FindAsync(Expression<Func<SystemUser, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.SystemUsers
            .Where(x => !x.IsDeleted)
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public async Task<SystemUser> AddAsync(SystemUser entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.SystemUsers.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
        catch (Exception ex)
        {
            // Log the exception (you can use any logging framework)
            Console.WriteLine($"Error adding SystemUser: {ex.Message}");
            throw; // Re-throw the exception after logging it
        }
    }

    public async Task UpdateAsync(SystemUser entity, CancellationToken cancellationToken = default)
    {
        _context.SystemUsers.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            await UpdateAsync(entity, cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.SystemUsers
            .AnyAsync(x => x.Oid == id && !x.IsDeleted, cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SystemUsers
            .CountAsync(x => !x.IsDeleted, cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<SystemUser, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.SystemUsers
            .Where(x => !x.IsDeleted)
            .CountAsync(predicate, cancellationToken);
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