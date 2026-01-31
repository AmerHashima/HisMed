using AutoMapper;
using HIS.Application.Commands.Doctor;
using HIS.Application.DTOs.Doctor;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Doctor;

public class CreateDoctorHandler : IRequestHandler<CreateDoctorCommand, DoctorDto>
{
    private readonly IDoctorRepository _repository;
    private readonly IMapper _mapper;

    public CreateDoctorHandler(IDoctorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DoctorDto> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.LicenseNumberExistsAsync(request.Doctor.LicenseNumber, cancellationToken: cancellationToken))
        {
            throw new InvalidOperationException($"Doctor with license number '{request.Doctor.LicenseNumber}' already exists");
        }

        var doctor = _mapper.Map<Domain.Entities.Doctor>(request.Doctor);
        var createdDoctor = await _repository.AddAsync(doctor, cancellationToken);
        
        return _mapper.Map<DoctorDto>(createdDoctor);
    }
}