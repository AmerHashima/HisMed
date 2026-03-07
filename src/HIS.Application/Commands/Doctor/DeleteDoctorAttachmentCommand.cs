using MediatR;

namespace HIS.Application.Commands.Doctor;

public record DeleteDoctorAttachmentCommand(Guid Id) : IRequest<bool>;
