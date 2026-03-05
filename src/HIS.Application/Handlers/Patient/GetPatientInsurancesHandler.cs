using AutoMapper;
using HIS.Application.DTOs.Patient;
using HIS.Application.Queries.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class GetPatientInsurancesHandler : IRequestHandler<GetPatientInsurancesQuery, IEnumerable<PatientInsuranceDto>>
{
    private readonly IPatientInsuranceRepository _repository;
    private readonly IMapper _mapper;

    public GetPatientInsurancesHandler(IPatientInsuranceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PatientInsuranceDto>> Handle(GetPatientInsurancesQuery request, CancellationToken cancellationToken)
    {
        var insurances = await _repository.GetByPatientIdAsync(request.PatientId, cancellationToken);
        return _mapper.Map<IEnumerable<PatientInsuranceDto>>(insurances);
    }
}
