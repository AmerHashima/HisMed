using AutoMapper;
using HIS.Application.DTOs.Patient;
using HIS.Application.Queries.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class GetPatientAddressesHandler : IRequestHandler<GetPatientAddressesQuery, IEnumerable<PatientAddressDto>>
{
    private readonly IPatientAddressRepository _repository;
    private readonly IMapper _mapper;

    public GetPatientAddressesHandler(IPatientAddressRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PatientAddressDto>> Handle(GetPatientAddressesQuery request, CancellationToken cancellationToken)
    {
        var addresses = await _repository.GetByPatientIdAsync(request.PatientId, cancellationToken);
        return _mapper.Map<IEnumerable<PatientAddressDto>>(addresses);
    }
}
