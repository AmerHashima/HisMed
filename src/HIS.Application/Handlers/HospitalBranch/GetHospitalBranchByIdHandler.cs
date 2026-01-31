using AutoMapper;
using HIS.Application.DTOs.HospitalBranch;
using HIS.Application.Queries.HospitalBranch;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.HospitalBranch;

public class GetHospitalBranchByIdHandler : IRequestHandler<GetHospitalBranchByIdQuery, HospitalBranchDto?>
{
    private readonly IHospitalBranchRepository _repository;
    private readonly IMapper _mapper;

    public GetHospitalBranchByIdHandler(IHospitalBranchRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<HospitalBranchDto?> Handle(GetHospitalBranchByIdQuery request, CancellationToken cancellationToken)
    {
        var branch = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return branch == null ? null : _mapper.Map<HospitalBranchDto>(branch);
    }
}