using HIS.Application.DTOs.SystemUser;
using MediatR;

namespace HIS.Application.Commands.SystemUser;

public record UpdateSystemUserCommand(UpdateSystemUserDto SystemUser) : IRequest<SystemUserDto>;