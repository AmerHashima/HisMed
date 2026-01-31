using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface IPatientRepository : IBaseRepository<Patient>
{
    Task<Patient?> GetByMRNAsync(string mrn, CancellationToken cancellationToken = default);
    Task<Patient?> GetByIdentityNumberAsync(string identityNumber, CancellationToken cancellationToken = default);
    Task<IEnumerable<Patient>> GetActivePatientsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<bool> MRNExistsAsync(string mrn, CancellationToken cancellationToken = default);
    Task<bool> IdentityNumberExistsAsync(string identityNumber, CancellationToken cancellationToken = default);
    Task<IEnumerable<Patient>> GetPatientsByGenderAsync(Guid genderLookupId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Patient>> GetPatientsByBloodGroupAsync(Guid bloodGroupLookupId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Patient>> GetPatientsByNationalityAsync(Guid nationalityLookupId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Patient>> GetPatientsByBranchAsync(Guid branchId, CancellationToken cancellationToken = default);
}