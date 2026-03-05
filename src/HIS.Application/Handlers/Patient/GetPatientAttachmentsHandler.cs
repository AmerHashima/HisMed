using AutoMapper;
using HIS.Application.DTOs.Patient;
using HIS.Application.Queries.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class GetPatientAttachmentsHandler : IRequestHandler<GetPatientAttachmentsQuery, IEnumerable<PatientAttachmentDto>>
{
    private readonly IPatientAttachmentRepository _repository;
    private readonly IMapper _mapper;

    public GetPatientAttachmentsHandler(IPatientAttachmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PatientAttachmentDto>> Handle(GetPatientAttachmentsQuery request, CancellationToken cancellationToken)
    {
        var attachments = await _repository.GetByPatientIdAsync(request.PatientId, cancellationToken);
        return _mapper.Map<IEnumerable<PatientAttachmentDto>>(attachments);
    }
}
