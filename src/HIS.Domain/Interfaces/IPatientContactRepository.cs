using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface IPatientContactRepository : IBaseRepository<PatientContact>
{
    Task<IEnumerable<PatientContact>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
}
