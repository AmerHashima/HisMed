using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HIS.Infrastructure.Repositories;

public class PatientAttachmentRepository : BaseRepository<PatientAttachment>, IPatientAttachmentRepository
{
    public PatientAttachmentRepository(HISDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<PatientAttachment>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<PatientAttachment>()
            .Include(a => a.AttachmentType)
            .Where(a => a.PatientId == patientId && !a.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}
