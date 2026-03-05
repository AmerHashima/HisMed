using MediatR;

namespace HIS.Application.Commands.Patient;

public record DeletePatientAddressCommand(Guid Id) : IRequest<bool>;
