using HIS.Application.DTOs.SystemUser;
using MediatR;

namespace HIS.Application.Commands.SystemUser;

public class UpdateSystemUserCommand : IRequest<SystemUserDto>
{
    public UpdateSystemUserDto UpdateSystemUserDto { get; set; } = new();
}