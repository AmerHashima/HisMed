using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HIS.Infrastructure.Repositories;

public class AppLookupDetailRepository : BaseRepository<AppLookupDetail>, IAppLookupDetailRepository
{
    public AppLookupDetailRepository(HISDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<AppLookupDetail>> GetByMasterIdAsync(Guid masterID, CancellationToken cancellationToken = default)
    {
        return await _context.AppLookupDetails
            .Include(x => x.LookupMaster)
            .Where(x => !x.IsDeleted && x.LookupMasterID == masterID)
            .OrderBy(x => x.SortOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<AppLookupDetail>> GetByLookupCodeAsync(string lookupCode, CancellationToken cancellationToken = default)
    {
        return await _context.AppLookupDetails
            .Include(x => x.LookupMaster)
            .Where(x => !x.IsDeleted && x.LookupMaster.LookupCode == lookupCode)
            .OrderBy(x => x.SortOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<AppLookupDetail?> GetByValueCodeAsync(Guid masterID, string valueCode, CancellationToken cancellationToken = default)
    {
        return await _context.AppLookupDetails
            .Include(x => x.LookupMaster)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.LookupMasterID == masterID && x.ValueCode == valueCode, cancellationToken);
    }

    public async Task<AppLookupDetail?> GetDefaultValueAsync(Guid masterID, CancellationToken cancellationToken = default)
    {
        return await _context.AppLookupDetails
            .Include(x => x.LookupMaster)
            .Where(x => !x.IsDeleted && x.LookupMasterID == masterID)
            .FirstOrDefaultAsync(x => x.IsDefault, cancellationToken);
    }

    public async Task<bool> ValueCodeExistsAsync(Guid masterID, string valueCode, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.AppLookupDetails
            .Where(x => !x.IsDeleted && x.LookupMasterID == masterID && x.ValueCode == valueCode);

        if (excludeId.HasValue)
        {
            query = query.Where(x => x.Oid != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<IEnumerable<AppLookupDetail>> GetOrderedByMasterIdAsync(Guid masterID, CancellationToken cancellationToken = default)
    {
        return await _context.AppLookupDetails
            .Include(x => x.LookupMaster)
            .Where(x => !x.IsDeleted && x.LookupMasterID == masterID)
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.ValueNameEn)
            .ToListAsync(cancellationToken);
    }
}