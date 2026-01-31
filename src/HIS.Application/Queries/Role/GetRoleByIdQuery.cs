using HIS.Application.DTOs.Role;
using MediatR;

namespace HIS.Application.Queries.Role;

public record GetRoleByIdQuery(Guid Id) : IRequest<RoleDto?>;