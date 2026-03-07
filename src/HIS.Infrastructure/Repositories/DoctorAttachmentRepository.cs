using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HIS.Infrastructure.Repositories;

public class DoctorAttachmentRepository : BaseRepository<DoctorAttachment>, IDoctorAttachmentRepository
{
    public DoctorAttachmentRepository(HISDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<DoctorAttachment>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<DoctorAttachment>()
            .Include(da => da.AttachmentType)
            .Where(da => da.DoctorId == doctorId && !da.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}
