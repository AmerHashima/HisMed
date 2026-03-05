using AutoMapper;
using HIS.Application.DTOs.Patient;
using HIS.Application.Queries.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class GetPatientContactsHandler : IRequestHandler<GetPatientContactsQuery, IEnumerable<PatientContactDto>>
{
    private readonly IPatientContactRepository _repository;
    private readonly IMapper _mapper;

    public GetPatientContactsHandler(IPatientContactRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PatientContactDto>> Handle(GetPatientContactsQuery request, CancellationToken cancellationToken)
    {
        var contacts = await _repository.GetByPatientIdAsync(request.PatientId, cancellationToken);
        return _mapper.Map<IEnumerable<PatientContactDto>>(contacts);
    }
}
