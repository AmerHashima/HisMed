using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface IPatientAddressRepository : IBaseRepository<PatientAddress>
{
    Task<IEnumerable<PatientAddress>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
}
