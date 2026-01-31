using AutoMapper;
using HIS.Application.Commands.Encounter;
using HIS.Application.DTOs.Encounter;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Encounter;

public class UpdateEncounterHandler : IRequestHandler<UpdateEncounterCommand, EncounterDto>
{
    private readonly IEncounterRepository _repository;
    private readonly IMapper _mapper;

    public UpdateEncounterHandler(IEncounterRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EncounterDto> Handle(UpdateEncounterCommand request, CancellationToken cancellationToken)
    {
        var existingEncounter = await _repository.GetByIdAsync(request.Encounter.Oid, cancellationToken);
        if (existingEncounter == null)
        {
            throw new KeyNotFoundException($"Encounter with ID {request.Encounter.Oid} not found");
        }

        existingEncounter.EncounterDate = request.Encounter.EncounterDate;
        existingEncounter.EncounterType = request.Encounter.EncounterType;
        existingEncounter.Notes = request.Encounter.Notes;
        existingEncounter.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existingEncounter, cancellationToken);
        return _mapper.Map<EncounterDto>(existingEncounter);
    }
}