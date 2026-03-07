using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface IDoctorAttachmentRepository : IBaseRepository<DoctorAttachment>
{
    Task<IEnumerable<DoctorAttachment>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
}
