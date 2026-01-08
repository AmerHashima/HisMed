using HIS.Application.DTOs.SystemUser;
using MediatR;

namespace HIS.Application.Commands.SystemUser;

public class CreateSystemUserCommand : IRequest<SystemUserDto>
{
    public CreateSystemUserDto CreateSystemUserDto { get; set; } = new();
}