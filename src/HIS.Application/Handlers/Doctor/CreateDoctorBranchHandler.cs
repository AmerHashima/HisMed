using AutoMapper;
using HIS.Application.Commands.Doctor;
using HIS.Application.DTOs.Doctor;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Doctor;

public class CreateDoctorBranchHandler : IRequestHandler<CreateDoctorBranchCommand, DoctorBranchDto>
{
    private readonly IDoctorBranchRepository _repository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public CreateDoctorBranchHandler(
        IDoctorBranchRepository repository,
        IDoctorRepository doctorRepository,
        IMapper mapper)
    {
        _repository = repository;
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<DoctorBranchDto> Handle(CreateDoctorBranchCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorBranch.DoctorId, cancellationToken);
        if (doctor == null)
            throw new InvalidOperationException($"Doctor with ID '{request.DoctorBranch.DoctorId}' not found");

        var entity = _mapper.Map<Domain.Entities.DoctorBranch>(request.DoctorBranch);
        var created = await _repository.AddAsync(entity, cancellationToken);

        return _mapper.Map<DoctorBranchDto>(created);
    }
}
