using HIS.Application.DTOs.Role;
using MediatR;

namespace HIS.Application.Queries.Role;

public record GetRoleListQuery() : IRequest<IEnumerable<RoleDto>>;