using AutoMapper;
using HIS.Application.Commands.Patient;
using HIS.Application.DTOs.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class CreatePatientContactHandler : IRequestHandler<CreatePatientContactCommand, PatientContactDto>
{
    private readonly IPatientContactRepository _repository;
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public CreatePatientContactHandler(
        IPatientContactRepository repository,
        IPatientRepository patientRepository,
        IMapper mapper)
    {
        _repository = repository;
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<PatientContactDto> Handle(CreatePatientContactCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Contact.PatientId, cancellationToken);
        if (patient == null)
            throw new InvalidOperationException($"Patient with ID '{request.Contact.PatientId}' not found");

        var entity = _mapper.Map<Domain.Entities.PatientContact>(request.Contact);
        var created = await _repository.AddAsync(entity, cancellationToken);

        return _mapper.Map<PatientContactDto>(created);
    }
}
