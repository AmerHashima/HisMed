using AutoMapper;
using HIS.Application.Commands.Diagnosis;
using HIS.Application.DTOs.Diagnosis;
using HIS.Application.DTOs.Encounter;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Diagnosis;

public class CreateDiagnosisHandler : IRequestHandler<CreateDiagnosisCommand, DiagnosisDto>
{
    private readonly IEncounterRepository _encounterRepository;
    private readonly IMapper _mapper;

    public CreateDiagnosisHandler(IEncounterRepository encounterRepository, IMapper mapper)
    {
        _encounterRepository = encounterRepository;
        _mapper = mapper;
    }

    public async Task<DiagnosisDto> Handle(CreateDiagnosisCommand request, CancellationToken cancellationToken)
    {
        // Validate encounter exists
        var encounter = await _encounterRepository.GetByIdAsync(request.Diagnosis.EncounterId, cancellationToken);
        if (encounter == null)
        {
            throw new InvalidOperationException($"Encounter with ID {request.Diagnosis.EncounterId} not found");
        }

        var diagnosis = _mapper.Map<Domain.Entities.Diagnosis>(request.Diagnosis);
        
        // Add diagnosis to encounter (assuming you have a collection)
        if (encounter.Diagnoses == null)
        {
            encounter.Diagnoses = new List<Domain.Entities.Diagnosis>();
        }
        encounter.Diagnoses.Add(diagnosis);

        await _encounterRepository.UpdateAsync(encounter, cancellationToken);

        return _mapper.Map<DiagnosisDto>(diagnosis);
    }
}