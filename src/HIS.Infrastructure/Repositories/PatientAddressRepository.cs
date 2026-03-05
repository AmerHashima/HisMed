using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HIS.Infrastructure.Repositories;

public class PatientAddressRepository : BaseRepository<PatientAddress>, IPatientAddressRepository
{
    public PatientAddressRepository(HISDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<PatientAddress>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<PatientAddress>()
            .Include(a => a.Country)
            .Include(a => a.City)
            .Where(a => a.PatientId == patientId && !a.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}
