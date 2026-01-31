using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HIS.Infrastructure.Repositories;

public class SpecialtyRepository : BaseRepository<Specialty>, ISpecialtyRepository
{
    public SpecialtyRepository(HISDbContext context) : base(context)
    {
    }

    public async Task<Specialty?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _context.Specialties
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
    }

    public async Task<IEnumerable<Specialty>> GetActiveSpecialtiesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Specialties
            .Where(x => !x.IsDeleted && x.IsActive)
            .OrderBy(x => x.NameEn)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> SpecialtyCodeExistsAsync(string code, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Specialties.Where(x => !x.IsDeleted && x.Code == code);
        
        if (excludeId.HasValue)
        {
            query = query.Where(x => x.Oid != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }
}