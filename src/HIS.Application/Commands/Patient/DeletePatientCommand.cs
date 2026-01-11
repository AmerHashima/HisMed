using MediatR;

namespace HIS.Application.Commands.Patient;

public record DeletePatientCommand(Guid Id) : IRequest<bool>;