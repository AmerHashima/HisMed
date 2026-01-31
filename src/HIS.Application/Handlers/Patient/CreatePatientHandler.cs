using AutoMapper;
using HIS.Application.Commands.Patient;
using HIS.Application.DTOs.Patient;
using HIS.Application.Services;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class CreatePatientHandler : IRequestHandler<CreatePatientCommand, PatientDto>
{
    private readonly IPatientRepository _repository;
    private readonly IPatientValidationService _validationService;
    private readonly IMapper _mapper;

    public CreatePatientHandler(
        IPatientRepository repository,
        IPatientValidationService validationService,
        IMapper mapper)
    {
        _repository = repository;
        _validationService = validationService;
        _mapper = mapper;
    }

    public async Task<PatientDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        // Business rule validation
        await _validationService.ValidateCreatePatientBusinessRulesAsync(request.Patient, cancellationToken);

        // Validate identity number uniqueness
        if (await _repository.IdentityNumberExistsAsync(request.Patient.IdentityNumber, cancellationToken))
        {
            throw new InvalidOperationException("Patient with this identity number already exists");
        }

        // Generate unique MRN
        var mrn = await GenerateUniqueMRNAsync(cancellationToken);

        // Map and create patient
        var patient = _mapper.Map<Domain.Entities.Patient>(request.Patient);
        patient.MRN = mrn;

        // Calculate full names (if not set by computed column)
        patient.FullNameAr = $"{patient.FirstNameAr} {patient.MiddleNameAr} {patient.LastNameAr}".Trim().Replace("  ", " ");
        patient.FullNameEn = $"{patient.FirstNameEn} {patient.MiddleNameEn} {patient.LastNameEn}".Trim().Replace("  ", " ");

        var createdPatient = await _repository.AddAsync(patient, cancellationToken);

        return _mapper.Map<PatientDto>(createdPatient);
    }

    private async Task<string> GenerateUniqueMRNAsync(CancellationToken cancellationToken)
    {
        string mrn;
        int attempts = 0;
        const int maxAttempts = 10;

        do
        {
            mrn = GenerateMRN();
            attempts++;

            if (attempts >= maxAttempts)
            {
                throw new InvalidOperationException("Unable to generate unique MRN after multiple attempts");
            }
        }
        while (await _repository.MRNExistsAsync(mrn, cancellationToken));

        return mrn;
    }

    private static string GenerateMRN()
    {
        // Generate format: MRN + YYYYMMDD + 6 random digits
        var today = DateTime.Now;
        var dateStr = today.ToString("yyyyMMdd");
        var random = Random.Shared.Next(100000, 999999);
        return $"MRN{dateStr}{random}";
    }
}