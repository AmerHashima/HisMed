using AutoMapper;
using HIS.Application.DTOs.Doctor;
using HIS.Application.Queries.Doctor;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Doctor;

public class GetDoctorAttachmentsHandler : IRequestHandler<GetDoctorAttachmentsQuery, IEnumerable<DoctorAttachmentDto>>
{
    private readonly IDoctorAttachmentRepository _repository;
    private readonly IMapper _mapper;

    public GetDoctorAttachmentsHandler(IDoctorAttachmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DoctorAttachmentDto>> Handle(GetDoctorAttachmentsQuery request, CancellationToken cancellationToken)
    {
        var attachments = await _repository.GetByDoctorIdAsync(request.DoctorId, cancellationToken);
        return _mapper.Map<IEnumerable<DoctorAttachmentDto>>(attachments);
    }
}
