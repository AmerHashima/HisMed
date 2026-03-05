using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Commands.Patient;

public record CreatePatientAttachmentCommand(CreatePatientAttachmentDto Attachment) : IRequest<PatientAttachmentDto>;
