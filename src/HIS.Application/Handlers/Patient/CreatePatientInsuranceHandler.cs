using AutoMapper;
using HIS.Application.Commands.Patient;
using HIS.Application.DTOs.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class CreatePatientInsuranceHandler : IRequestHandler<CreatePatientInsuranceCommand, PatientInsuranceDto>
{
    private readonly IPatientInsuranceRepository _repository;
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public CreatePatientInsuranceHandler(
        IPatientInsuranceRepository repository,
        IPatientRepository patientRepository,
        IMapper mapper)
    {
        _repository = repository;
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<PatientInsuranceDto> Handle(CreatePatientInsuranceCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Insurance.PatientId, cancellationToken);
        if (patient == null)
            throw new InvalidOperationException($"Patient with ID '{request.Insurance.PatientId}' not found");

        var entity = _mapper.Map<Domain.Entities.PatientInsurance>(request.Insurance);
        var created = await _repository.AddAsync(entity, cancellationToken);

        return _mapper.Map<PatientInsuranceDto>(created);
    }
}
