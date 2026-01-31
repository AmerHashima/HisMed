using HIS.Application.DTOs.SystemUserSpace;
using MediatR;

namespace HIS.Application.Queries.SystemUserSpace;

public record GetSystemUserListQuery(
    bool IncludeInactive = false,
    int? RoleId = null
) : IRequest<IEnumerable<SystemUserDto>>;