using HIS.Application.DTOs.SystemUser;
using MediatR;

namespace HIS.Application.Commands.SystemUser;

public record CreateSystemUserCommand(CreateSystemUserDto SystemUser) : IRequest<SystemUserDto>;