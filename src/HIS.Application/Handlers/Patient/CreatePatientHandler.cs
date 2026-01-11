using AutoMapper;
using HIS.Application.Commands.Patient;
using HIS.Application.DTOs.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class CreatePatientHandler : IRequestHandler<CreatePatientCommand, PatientDto>
{
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;

    public CreatePatientHandler(IPatientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PatientDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        // Validate identifier uniqueness based on type
        if (request.Patient.IdentifierType == "NationalID" && !string.IsNullOrEmpty(request.Patient.NationalID))
        {
            if (await _repository.NationalIDExistsAsync(request.Patient.NationalID, cancellationToken))
                throw new InvalidOperationException("Patient with this National ID already exists");
        }
        else if (request.Patient.IdentifierType == "Passport" && !string.IsNullOrEmpty(request.Patient.PassportNumber))
        {
            if (await _repository.PassportNumberExistsAsync(request.Patient.PassportNumber, cancellationToken))
                throw new InvalidOperationException("Patient with this Passport number already exists");
        }

        // Generate unique MRN
        string mrn;
        do
        {
            mrn = GenerateMRN();
        } while (await _repository.MRNExistsAsync(mrn, cancellationToken));

        var patient = _mapper.Map<Domain.Entities.Patient>(request.Patient);
        patient.MRN = mrn;

        var createdPatient = await _repository.AddAsync(patient, cancellationToken);
        return _mapper.Map<PatientDto>(createdPatient);
    }

    private static string GenerateMRN()
    {
        // Generate format: MRN + YYYYMMDD + 6 random digits
        var today = DateTime.Now;
        var dateStr = today.ToString("yyyyMMdd");
        var random = new Random().Next(100000, 999999);
        return $"MRN{dateStr}{random}";
    }
}