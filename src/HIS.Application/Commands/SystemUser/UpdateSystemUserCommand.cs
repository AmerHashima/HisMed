using HIS.Application.DTOs.SystemUserSpace;
using MediatR;

namespace HIS.Application.Commands.SystemUserSpace;

public record UpdateSystemUserCommand(UpdateSystemUserDto SystemUser) : IRequest<SystemUserDto>;