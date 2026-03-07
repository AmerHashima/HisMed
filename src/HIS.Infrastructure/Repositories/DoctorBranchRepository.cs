using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HIS.Infrastructure.Repositories;

public class DoctorBranchRepository : BaseRepository<DoctorBranch>, IDoctorBranchRepository
{
    public DoctorBranchRepository(HISDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<DoctorBranch>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<DoctorBranch>()
            .Include(db => db.Branch)
            .Where(db => db.DoctorId == doctorId && !db.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}
