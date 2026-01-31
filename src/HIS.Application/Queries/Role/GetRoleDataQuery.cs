using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.Role;
using MediatR;

namespace HIS.Application.Queries.Role;

public record GetRoleDataQuery(QueryRequest QueryRequest) : IRequest<PagedResult<RoleDto>>;