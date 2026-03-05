using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HIS.Infrastructure.Repositories;

public class PatientContactRepository : BaseRepository<PatientContact>, IPatientContactRepository
{
    public PatientContactRepository(HISDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<PatientContact>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<PatientContact>()
            .Include(c => c.Relationship)
            .Where(c => c.PatientId == patientId && !c.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}
