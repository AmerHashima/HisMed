using AutoMapper;
using HIS.Application.Commands.Patient;
using HIS.Application.DTOs.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class CreatePatientAttachmentHandler : IRequestHandler<CreatePatientAttachmentCommand, PatientAttachmentDto>
{
    private readonly IPatientAttachmentRepository _repository;
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public CreatePatientAttachmentHandler(
        IPatientAttachmentRepository repository,
        IPatientRepository patientRepository,
        IMapper mapper)
    {
        _repository = repository;
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<PatientAttachmentDto> Handle(CreatePatientAttachmentCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Attachment.PatientId, cancellationToken);
        if (patient == null)
            throw new InvalidOperationException($"Patient with ID '{request.Attachment.PatientId}' not found");

        var entity = _mapper.Map<Domain.Entities.PatientAttachment>(request.Attachment);
        entity.UploadedAt = DateTime.UtcNow;
        var created = await _repository.AddAsync(entity, cancellationToken);

        return _mapper.Map<PatientAttachmentDto>(created);
    }
}
