using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface IPatientInsuranceRepository : IBaseRepository<PatientInsurance>
{
    Task<IEnumerable<PatientInsurance>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
}
