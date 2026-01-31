using AutoMapper;
using HIS.Application.Commands.HospitalBranch;
using HIS.Application.DTOs.HospitalBranch;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.HospitalBranch;

public class CreateHospitalBranchHandler : IRequestHandler<CreateHospitalBranchCommand, HospitalBranchDto>
{
    private readonly IHospitalBranchRepository _repository;
    private readonly IMapper _mapper;

    public CreateHospitalBranchHandler(IHospitalBranchRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<HospitalBranchDto> Handle(CreateHospitalBranchCommand request, CancellationToken cancellationToken)
    {
        // Check if branch code already exists
        if (await _repository.BranchCodeExistsAsync(request.HospitalBranch.Code, cancellationToken: cancellationToken))
        {
            throw new InvalidOperationException($"Branch with code '{request.HospitalBranch.Code}' already exists");
        }

        var branch = _mapper.Map<Domain.Entities.HospitalBranch>(request.HospitalBranch);
        var createdBranch = await _repository.AddAsync(branch, cancellationToken);
        
        return _mapper.Map<HospitalBranchDto>(createdBranch);
    }
}