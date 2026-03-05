using MediatR;

namespace HIS.Application.Commands.Patient;

public record DeletePatientAttachmentCommand(Guid Id) : IRequest<bool>;
