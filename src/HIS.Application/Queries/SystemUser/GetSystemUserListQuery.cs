using HIS.Application.DTOs.SystemUser;
using MediatR;

namespace HIS.Application.Queries.SystemUser;

public record GetSystemUserListQuery(
    bool IncludeInactive = false,
    int? RoleId = null
) : IRequest<IEnumerable<SystemUserDto>>;