using HIS.Application.DTOs.SystemUserSpace;
using MediatR;

namespace HIS.Application.Commands.SystemUserSpace;

public record CreateSystemUserCommand(CreateSystemUserDto SystemUser) : IRequest<SystemUserDto>;