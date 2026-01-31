using MediatR;

namespace HIS.Application.Commands.Role;

public record DeleteRoleCommand(Guid Id) : IRequest<bool>;