using AutoMapper;
using HIS.Application.Commands.Encounter;
using HIS.Application.DTOs.Encounter;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Encounter;

public class CreateEncounterHandler : IRequestHandler<CreateEncounterCommand, EncounterDto>
{
    private readonly IEncounterRepository _repository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public CreateEncounterHandler(
        IEncounterRepository repository,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        IMapper mapper)
    {
        _repository = repository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<EncounterDto> Handle(CreateEncounterCommand request, CancellationToken cancellationToken)
    {
        // Validate patient exists
        var patient = await _patientRepository.GetByIdAsync(request.Encounter.PatientId, cancellationToken);
        if (patient == null)
        {
            throw new InvalidOperationException($"Patient with ID {request.Encounter.PatientId} not found");
        }

        // Validate doctor exists
        var doctor = await _doctorRepository.GetByIdAsync(request.Encounter.DoctorId, cancellationToken);
        if (doctor == null)
        {
            throw new InvalidOperationException($"Doctor with ID {request.Encounter.DoctorId} not found");
        }

        var encounter = _mapper.Map<Domain.Entities.Encounter>(request.Encounter);
        var createdEncounter = await _repository.AddAsync(encounter, cancellationToken);

        return _mapper.Map<EncounterDto>(createdEncounter);
    }
}