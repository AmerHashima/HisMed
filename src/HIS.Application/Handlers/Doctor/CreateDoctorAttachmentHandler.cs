using AutoMapper;
using HIS.Application.Commands.Doctor;
using HIS.Application.DTOs.Doctor;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Doctor;

public class CreateDoctorAttachmentHandler : IRequestHandler<CreateDoctorAttachmentCommand, DoctorAttachmentDto>
{
    private readonly IDoctorAttachmentRepository _repository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public CreateDoctorAttachmentHandler(
        IDoctorAttachmentRepository repository,
        IDoctorRepository doctorRepository,
        IMapper mapper)
    {
        _repository = repository;
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<DoctorAttachmentDto> Handle(CreateDoctorAttachmentCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Attachment.DoctorId, cancellationToken);
        if (doctor == null)
            throw new InvalidOperationException($"Doctor with ID '{request.Attachment.DoctorId}' not found");

        var entity = _mapper.Map<Domain.Entities.DoctorAttachment>(request.Attachment);
        entity.UploadedAt = DateTime.UtcNow;
        var created = await _repository.AddAsync(entity, cancellationToken);

        return _mapper.Map<DoctorAttachmentDto>(created);
    }
}
