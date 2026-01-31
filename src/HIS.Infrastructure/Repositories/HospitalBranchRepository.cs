using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HIS.Infrastructure.Repositories;

public class HospitalBranchRepository : BaseRepository<HospitalBranch>, IHospitalBranchRepository
{
    public HospitalBranchRepository(HISDbContext context) : base(context)
    {
    }

    public async Task<HospitalBranch?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _context.HospitalBranches
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
    }

    public async Task<IEnumerable<HospitalBranch>> GetActiveBranchesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.HospitalBranches
            .Where(x => !x.IsDeleted && x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> BranchCodeExistsAsync(string code, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.HospitalBranches.Where(x => !x.IsDeleted && x.Code == code);
        
        if (excludeId.HasValue)
        {
            query = query.Where(x => x.Oid != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }
}