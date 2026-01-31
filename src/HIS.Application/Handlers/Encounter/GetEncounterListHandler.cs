using AutoMapper;
using HIS.Application.DTOs.Encounter;
using HIS.Application.Queries.Encounter;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Encounter;

public class GetEncounterListHandler : IRequestHandler<GetEncounterListQuery, IEnumerable<EncounterDto>>
{
    private readonly IEncounterRepository _repository;
    private readonly IMapper _mapper;

    public GetEncounterListHandler(IEncounterRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EncounterDto>> Handle(GetEncounterListQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.Encounter> encounters;

        if (request.PatientId.HasValue)
        {
            encounters = await _repository.GetEncountersByPatientAsync(request.PatientId.Value, cancellationToken);
        }
        else if (request.DoctorId.HasValue)
        {
            encounters = await _repository.GetEncountersByDoctorAsync(request.DoctorId.Value, cancellationToken);
        }
        else
        {
            encounters = await _repository.GetAllAsync(cancellationToken);
        }

        return _mapper.Map<IEnumerable<EncounterDto>>(encounters);
    }
}