using AutoMapper;
using HIS.Application.Commands.Patient;
using HIS.Application.DTOs.Patient;
using HIS.Application.Services;
using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class CreateFullPatientHandler : IRequestHandler<CreateFullPatientCommand, FullPatientDto>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IPatientAddressRepository _addressRepository;
    private readonly IPatientContactRepository _contactRepository;
    private readonly IPatientAttachmentRepository _attachmentRepository;
    private readonly IPatientInsuranceRepository _insuranceRepository;
    private readonly IPatientValidationService _validationService;
    private readonly IMapper _mapper;

    public CreateFullPatientHandler(
        IPatientRepository patientRepository,
        IPatientAddressRepository addressRepository,
        IPatientContactRepository contactRepository,
        IPatientAttachmentRepository attachmentRepository,
        IPatientInsuranceRepository insuranceRepository,
        IPatientValidationService validationService,
        IMapper mapper)
    {
        _patientRepository = patientRepository;
        _addressRepository = addressRepository;
        _contactRepository = contactRepository;
        _attachmentRepository = attachmentRepository;
        _insuranceRepository = insuranceRepository;
        _validationService = validationService;
        _mapper = mapper;
    }

    public async Task<FullPatientDto> Handle(CreateFullPatientCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Patient;

        // Business rule validation
        var createPatientDto = _mapper.Map<CreatePatientDto>(dto);
        await _validationService.ValidateCreatePatientBusinessRulesAsync(createPatientDto, cancellationToken);

        // Validate identity number uniqueness
        if (await _patientRepository.IdentityNumberExistsAsync(dto.IdentityNumber, cancellationToken))
        {
            throw new InvalidOperationException("Patient with this identity number already exists");
        }

        // Generate unique MRN
        var mrn = await GenerateUniqueMRNAsync(cancellationToken);

        // Map and create patient
        var patient = _mapper.Map<Domain.Entities.Patient>(dto);
        patient.MRN = mrn;
        patient.FullNameAr = $"{patient.FirstNameAr} {patient.MiddleNameAr} {patient.LastNameAr}".Trim().Replace("  ", " ");
        patient.FullNameEn = $"{patient.FirstNameEn} {patient.MiddleNameEn} {patient.LastNameEn}".Trim().Replace("  ", " ");

        var createdPatient = await _patientRepository.AddAsync(patient, cancellationToken);
        var patientId = createdPatient.Oid;

        // Create addresses
        var createdAddresses = new List<PatientAddress>();
        if (dto.Addresses?.Count > 0)
        {
            foreach (var addressDto in dto.Addresses)
            {
                var address = _mapper.Map<PatientAddress>(addressDto);
                address.PatientId = patientId;
                createdAddresses.Add(await _addressRepository.AddAsync(address, cancellationToken));
            }
        }

        // Create contacts
        var createdContacts = new List<PatientContact>();
        if (dto.Contacts?.Count > 0)
        {
            foreach (var contactDto in dto.Contacts)
            {
                var contact = _mapper.Map<PatientContact>(contactDto);
                contact.PatientId = patientId;
                createdContacts.Add(await _contactRepository.AddAsync(contact, cancellationToken));
            }
        }

        // Create attachments
        var createdAttachments = new List<PatientAttachment>();
        if (dto.Attachments?.Count > 0)
        {
            foreach (var attachmentDto in dto.Attachments)
            {
                var attachment = _mapper.Map<PatientAttachment>(attachmentDto);
                attachment.PatientId = patientId;
                attachment.UploadedAt = DateTime.UtcNow;
                createdAttachments.Add(await _attachmentRepository.AddAsync(attachment, cancellationToken));
            }
        }

        // Create insurances
        var createdInsurances = new List<PatientInsurance>();
        if (dto.Insurances?.Count > 0)
        {
            foreach (var insuranceDto in dto.Insurances)
            {
                var insurance = _mapper.Map<PatientInsurance>(insuranceDto);
                insurance.PatientId = patientId;
                createdInsurances.Add(await _insuranceRepository.AddAsync(insurance, cancellationToken));
            }
        }

        // Build response
        var result = _mapper.Map<FullPatientDto>(createdPatient);
        result.Addresses = _mapper.Map<List<PatientAddressDto>>(createdAddresses);
        result.Contacts = _mapper.Map<List<PatientContactDto>>(createdContacts);
        result.Attachments = _mapper.Map<List<PatientAttachmentDto>>(createdAttachments);
        result.Insurances = _mapper.Map<List<PatientInsuranceDto>>(createdInsurances);

        return result;
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
        while (await _patientRepository.MRNExistsAsync(mrn, cancellationToken));

        return mrn;
    }

    private static string GenerateMRN()
    {
        var today = DateTime.Now;
        var dateStr = today.ToString("yyyyMMdd");
        var random = Random.Shared.Next(100000, 999999);
        return $"MRN{dateStr}{random}";
    }
}