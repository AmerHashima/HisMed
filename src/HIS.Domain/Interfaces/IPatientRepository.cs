using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface IPatientRepository : IBaseRepository<Patient>
{
    Task<Patient?> GetByMRNAsync(string mrn, CancellationToken cancellationToken = default);
    Task<Patient?> GetByNationalIDAsync(string nationalId, CancellationToken cancellationToken = default);
    Task<Patient?> GetByPassportNumberAsync(string passportNumber, CancellationToken cancellationToken = default);
    Task<IEnumerable<Patient>> GetActivePatientsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<bool> MRNExistsAsync(string mrn, CancellationToken cancellationToken = default);
    Task<bool> NationalIDExistsAsync(string nationalId, CancellationToken cancellationToken = default);
    Task<bool> PassportNumberExistsAsync(string passportNumber, CancellationToken cancellationToken = default);
    Task<IEnumerable<Patient>> GetPatientsByGenderAsync(char gender, CancellationToken cancellationToken = default);
    Task<IEnumerable<Patient>> GetPatientsByBloodGroupAsync(string bloodGroup, CancellationToken cancellationToken = default);
    Task<IEnumerable<Patient>> GetPatientsByNationalityAsync(string nationality, CancellationToken cancellationToken = default);
}