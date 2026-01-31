using AutoMapper;
using HIS.Application.DTOs.HospitalBranch;
using HIS.Application.Queries.HospitalBranch;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.HospitalBranch;

public class GetHospitalBranchListHandler : IRequestHandler<GetHospitalBranchListQuery, IEnumerable<HospitalBranchDto>>
{
    private readonly IHospitalBranchRepository _repository;
    private readonly IMapper _mapper;

    public GetHospitalBranchListHandler(IHospitalBranchRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<HospitalBranchDto>> Handle(GetHospitalBranchListQuery request, CancellationToken cancellationToken)
    {
        var branches = request.ActiveOnly
            ? await _repository.GetActiveBranchesAsync(cancellationToken)
            : await _repository.GetAllAsync(cancellationToken);

        return _mapper.Map<IEnumerable<HospitalBranchDto>>(branches);
    }
}