using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface IDoctorRepository : IBaseRepository<Doctor>
{
    Task<Doctor?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Doctor?> GetByLicenseNumberAsync(string licenseNumber, CancellationToken cancellationToken = default);
    Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyAsync(Guid specialtyId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Doctor>> GetDoctorsByBranchAsync(Guid branchId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Doctor>> GetActiveDoctorsAsync(CancellationToken cancellationToken = default);
    Task<bool> LicenseNumberExistsAsync(string licenseNumber, Guid? excludeId = null, CancellationToken cancellationToken = default);
}