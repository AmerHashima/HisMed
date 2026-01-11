using MediatR;

namespace HIS.Application.Commands.SystemUser;

public record DeleteSystemUserCommand(Guid Id) : IRequest<bool>;