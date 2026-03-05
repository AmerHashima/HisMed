using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface IPatientAttachmentRepository : IBaseRepository<PatientAttachment>
{
    Task<IEnumerable<PatientAttachment>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
}
