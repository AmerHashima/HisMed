using AutoMapper;
using HIS.Application.Commands.HospitalBranch;
using HIS.Application.DTOs.HospitalBranch;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.HospitalBranch;

public class UpdateHospitalBranchHandler : IRequestHandler<UpdateHospitalBranchCommand, HospitalBranchDto>
{
    private readonly IHospitalBranchRepository _repository;
    private readonly IMapper _mapper;

    public UpdateHospitalBranchHandler(IHospitalBranchRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<HospitalBranchDto> Handle(UpdateHospitalBranchCommand request, CancellationToken cancellationToken)
    {
        var existingBranch = await _repository.GetByIdAsync(request.HospitalBranch.Oid, cancellationToken);
        if (existingBranch == null)
        {
            throw new KeyNotFoundException($"Hospital Branch with ID {request.HospitalBranch.Oid} not found");
        }

        // Check if branch code is unique (excluding current branch)
        if (await _repository.BranchCodeExistsAsync(request.HospitalBranch.Code, request.HospitalBranch.Oid, cancellationToken))
        {
            throw new InvalidOperationException($"Branch code '{request.HospitalBranch.Code}' is already in use");
        }

        // Update properties
        existingBranch.Code = request.HospitalBranch.Code;
        existingBranch.Name = request.HospitalBranch.Name;
        existingBranch.Address = request.HospitalBranch.Address;
        existingBranch.City = request.HospitalBranch.City;
        existingBranch.State = request.HospitalBranch.State;
        existingBranch.PostalCode = request.HospitalBranch.PostalCode;
        existingBranch.Country = request.HospitalBranch.Country;
        existingBranch.IsActive = request.HospitalBranch.IsActive;
        existingBranch.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existingBranch, cancellationToken);
        return _mapper.Map<HospitalBranchDto>(existingBranch);
    }
}