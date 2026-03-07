using HIS.Application.DTOs.Doctor;
using MediatR;

namespace HIS.Application.Commands.Doctor;

public record CreateDoctorAttachmentCommand(CreateDoctorAttachmentDto Attachment) : IRequest<DoctorAttachmentDto>;
