using HIS.Application.DTOs.Role;
using MediatR;

namespace HIS.Application.Commands.Role;

public record UpdateRoleCommand(UpdateRoleDto Role) : IRequest<RoleDto>;