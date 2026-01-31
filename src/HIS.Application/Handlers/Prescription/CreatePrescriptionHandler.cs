using AutoMapper;
using HIS.Application.Commands.Prescription;
using HIS.Application.DTOs.Encounter;
using HIS.Application.DTOs.Prescription;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Prescription;

public class CreatePrescriptionHandler : IRequestHandler<CreatePrescriptionCommand, PrescriptionDto>
{
    private readonly IEncounterRepository _encounterRepository;
    private readonly IMapper _mapper;

    public CreatePrescriptionHandler(IEncounterRepository encounterRepository, IMapper mapper)
    {
        _encounterRepository = encounterRepository;
        _mapper = mapper;
    }

    public async Task<PrescriptionDto> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        // Validate encounter exists
        var encounter = await _encounterRepository.GetByIdAsync(request.Prescription.EncounterId, cancellationToken);
        if (encounter == null)
        {
            throw new InvalidOperationException($"Encounter with ID {request.Prescription.EncounterId} not found");
        }

        var prescription = _mapper.Map<Domain.Entities.Prescription>(request.Prescription);
        
        // Add prescription to encounter
        if (encounter.Prescriptions == null)
        {
            encounter.Prescriptions = new List<Domain.Entities.Prescription>();
        }
        encounter.Prescriptions.Add(prescription);

        await _encounterRepository.UpdateAsync(encounter, cancellationToken);

        return _mapper.Map<PrescriptionDto>(prescription);
    }
}