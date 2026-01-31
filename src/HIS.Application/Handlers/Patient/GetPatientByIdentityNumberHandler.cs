using AutoMapper;
using HIS.Application.DTOs.Patient;
using HIS.Application.Queries.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class GetPatientByIdentityNumberHandler : IRequestHandler<GetPatientByIdentityNumberQuery, PatientDto?>
{
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;

    public GetPatientByIdentityNumberHandler(IPatientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PatientDto?> Handle(GetPatientByIdentityNumberQuery request, CancellationToken cancellationToken)
    {
        var patient = await _repository.GetByIdentityNumberAsync(request.IdentityNumber, cancellationToken);
        return patient == null ? null : _mapper.Map<PatientDto>(patient);
    }
}