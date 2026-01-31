using HIS.Application.Common.Exceptions;
using HIS.Application.DTOs.Patient;
using HIS.Application.Services;
using HIS.Domain.Interfaces;

namespace HIS.Infrastructure.Services;

public class PatientValidationService : IPatientValidationService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IAppLookupDetailRepository _lookupRepository;

    public PatientValidationService(
        IPatientRepository patientRepository,
        IAppLookupDetailRepository lookupRepository)
    {
        _patientRepository = patientRepository;
        _lookupRepository = lookupRepository;
    }

    public async Task ValidateCreatePatientBusinessRulesAsync(CreatePatientDto patient, CancellationToken cancellationToken = default)
    {
        // Validate identity number uniqueness
        if (!await IsIdentityNumberUniqueAsync(patient.IdentityNumber, cancellationToken: cancellationToken))
        {
            throw new BusinessValidationException("IdentityNumber", "A patient with this identity number already exists");
        }

        // Validate foreign key lookups exist
        await ValidateLookupReferencesAsync(patient, cancellationToken);

        // Business rule: Age must be valid
        var age = DateTime.Today.Year - patient.BirthDate.Year;
        if (age < 0 || age > 150)
        {
            throw new BusinessValidationException("BirthDate", "Invalid age calculated from birth date");
        }
    }

    public async Task ValidateUpdatePatientBusinessRulesAsync(UpdatePatientDto patient, CancellationToken cancellationToken = default)
    {
        // Check if patient exists
        var existingPatient = await _patientRepository.GetByIdAsync(patient.Oid, cancellationToken);
        if (existingPatient == null)
        {
            throw new NotFoundException("Patient", patient.Oid);
        }

        // Validate identity number uniqueness (excluding current patient)
        if (!await IsIdentityNumberUniqueAsync(patient.IdentityNumber, patient.Oid, cancellationToken))
        {
            throw new BusinessValidationException("IdentityNumber", "A patient with this identity number already exists");
        }

        // Validate foreign key lookups exist
        await ValidateLookupReferencesAsync(patient, cancellationToken);
    }

    public async Task<bool> IsIdentityNumberUniqueAsync(string identityNumber, Guid? excludePatientId = null, CancellationToken cancellationToken = default)
    {
        var existingPatient = await _patientRepository.GetByIdentityNumberAsync(identityNumber, cancellationToken);
        
        if (existingPatient == null)
            return true;

        // If excluding a patient ID, check if it's the same patient
        return excludePatientId.HasValue && existingPatient.Oid == excludePatientId.Value;
    }

    public async Task<bool> IsMobileUniqueAsync(string mobile, Guid? excludePatientId = null, CancellationToken cancellationToken = default)
    {
        var patients = await _patientRepository.FindAsync(p => p.Mobile == mobile, cancellationToken);
        var existingPatient = patients.FirstOrDefault();
        
        if (existingPatient == null)
            return true;

        return excludePatientId.HasValue && existingPatient.Oid == excludePatientId.Value;
    }

    private async Task ValidateLookupReferencesAsync(CreatePatientDto patient, CancellationToken cancellationToken)
    {
        // Validate IdentityType (required)
        if (!await _lookupRepository.ExistsAsync(patient.IdentityTypeLookupId, cancellationToken))
        {
            throw new BusinessValidationException("IdentityTypeLookupId", "Invalid identity type reference");
        }

        // Validate Gender (required)
        if (!await _lookupRepository.ExistsAsync(patient.GenderLookupId, cancellationToken))
        {
            throw new BusinessValidationException("GenderLookupId", "Invalid gender reference");
        }

        // Validate MaritalStatus if provided
        if (patient.MaritalStatusLookupId.HasValue)
        {
            if (!await _lookupRepository.ExistsAsync(patient.MaritalStatusLookupId.Value, cancellationToken))
            {
                throw new BusinessValidationException("MaritalStatusLookupId", "Invalid marital status reference");
            }
        }

        // Validate Nationality if provided
        if (patient.NationalityLookupId.HasValue)
        {
            if (!await _lookupRepository.ExistsAsync(patient.NationalityLookupId.Value, cancellationToken))
            {
                throw new BusinessValidationException("NationalityLookupId", "Invalid nationality reference");
            }
        }

        // Validate BloodGroup if provided
        if (patient.BloodGroupLookupId.HasValue)
        {
            if (!await _lookupRepository.ExistsAsync(patient.BloodGroupLookupId.Value, cancellationToken))
            {
                throw new BusinessValidationException("BloodGroupLookupId", "Invalid blood group reference");
            }
        }

        // Validate Branch (required)
        // Note: This would require IHospitalBranchRepository - add it as a dependency
    }

    private async Task ValidateLookupReferencesAsync(UpdatePatientDto patient, CancellationToken cancellationToken)
    {
        var createDto = new CreatePatientDto
        {
            IdentityTypeLookupId = patient.IdentityTypeLookupId,
            IdentityNumber = patient.IdentityNumber,
            GenderLookupId = patient.GenderLookupId,
            MaritalStatusLookupId = patient.MaritalStatusLookupId,
            NationalityLookupId = patient.NationalityLookupId,
            BloodGroupLookupId = patient.BloodGroupLookupId,
            BranchId = patient.BranchId
        };

        await ValidateLookupReferencesAsync(createDto, cancellationToken);
    }
}