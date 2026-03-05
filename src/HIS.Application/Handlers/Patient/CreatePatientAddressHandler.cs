using AutoMapper;
using HIS.Application.Commands.Patient;
using HIS.Application.DTOs.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class CreatePatientAddressHandler : IRequestHandler<CreatePatientAddressCommand, PatientAddressDto>
{
    private readonly IPatientAddressRepository _repository;
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public CreatePatientAddressHandler(
        IPatientAddressRepository repository,
        IPatientRepository patientRepository,
        IMapper mapper)
    {
        _repository = repository;
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<PatientAddressDto> Handle(CreatePatientAddressCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Address.PatientId, cancellationToken);
        if (patient == null)
            throw new InvalidOperationException($"Patient with ID '{request.Address.PatientId}' not found");

        var entity = _mapper.Map<Domain.Entities.PatientAddress>(request.Address);
        var created = await _repository.AddAsync(entity, cancellationToken);

        return _mapper.Map<PatientAddressDto>(created);
    }
}
