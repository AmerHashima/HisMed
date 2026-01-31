using MediatR;

namespace HIS.Application.Commands.Specialty;

public record DeleteSpecialtyCommand(Guid Id) : IRequest<bool>;