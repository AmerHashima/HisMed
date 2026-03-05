using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HIS.Infrastructure.Repositories;

public class PatientInsuranceRepository : BaseRepository<PatientInsurance>, IPatientInsuranceRepository
{
    public PatientInsuranceRepository(HISDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<PatientInsurance>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<PatientInsurance>()
            .Include(i => i.InsuranceCompany)
            .Where(i => i.PatientId == patientId && !i.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}
