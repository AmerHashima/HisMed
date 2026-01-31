using HIS.Application.DTOs.Patient;

namespace HIS.Application.Services;

public interface IPatientValidationService
{
    Task ValidateCreatePatientBusinessRulesAsync(CreatePatientDto patient, CancellationToken cancellationToken = default);
    Task ValidateUpdatePatientBusinessRulesAsync(UpdatePatientDto patient, CancellationToken cancellationToken = default);
    Task<bool> IsIdentityNumberUniqueAsync(string identityNumber, Guid? excludePatientId = null, CancellationToken cancellationToken = default);
    Task<bool> IsMobileUniqueAsync(string mobile, Guid? excludePatientId = null, CancellationToken cancellationToken = default);
}