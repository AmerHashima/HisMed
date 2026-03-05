using MediatR;

namespace HIS.Application.Commands.Patient;

public record DeletePatientContactCommand(Guid Id) : IRequest<bool>;
