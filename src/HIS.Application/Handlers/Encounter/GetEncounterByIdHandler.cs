using AutoMapper;
using HIS.Application.DTOs.Encounter;
using HIS.Application.Queries.Encounter;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Encounter;

public class GetEncounterByIdHandler : IRequestHandler<GetEncounterByIdQuery, EncounterDto?>
{
    private readonly IEncounterRepository _repository;
    private readonly IMapper _mapper;

    public GetEncounterByIdHandler(IEncounterRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EncounterDto?> Handle(GetEncounterByIdQuery request, CancellationToken cancellationToken)
    {
        var encounter = await _repository.GetEncounterWithDetailsAsync(request.Id, cancellationToken);
        return encounter == null ? null : _mapper.Map<EncounterDto>(encounter);
    }
}