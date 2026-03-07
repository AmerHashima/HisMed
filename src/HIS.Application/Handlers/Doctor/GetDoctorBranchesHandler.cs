using AutoMapper;
using HIS.Application.DTOs.Doctor;
using HIS.Application.Queries.Doctor;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Doctor;

public class GetDoctorBranchesHandler : IRequestHandler<GetDoctorBranchesQuery, IEnumerable<DoctorBranchDto>>
{
    private readonly IDoctorBranchRepository _repository;
    private readonly IMapper _mapper;

    public GetDoctorBranchesHandler(IDoctorBranchRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DoctorBranchDto>> Handle(GetDoctorBranchesQuery request, CancellationToken cancellationToken)
    {
        var branches = await _repository.GetByDoctorIdAsync(request.DoctorId, cancellationToken);
        return _mapper.Map<IEnumerable<DoctorBranchDto>>(branches);
    }
}
