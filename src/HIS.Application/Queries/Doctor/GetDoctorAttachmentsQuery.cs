using HIS.Application.DTOs.Doctor;
using MediatR;

namespace HIS.Application.Queries.Doctor;

public record GetDoctorAttachmentsQuery(Guid DoctorId) : IRequest<IEnumerable<DoctorAttachmentDto>>;
