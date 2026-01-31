using MediatR;

namespace HIS.Application.Commands.SystemUserSpace;

public record DeleteSystemUserCommand(Guid Id) : IRequest<bool>;