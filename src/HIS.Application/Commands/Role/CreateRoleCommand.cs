using HIS.Application.DTOs.Role;
using MediatR;

namespace HIS.Application.Commands.Role;

public record CreateRoleCommand(CreateRoleDto Role) : IRequest<RoleDto>;