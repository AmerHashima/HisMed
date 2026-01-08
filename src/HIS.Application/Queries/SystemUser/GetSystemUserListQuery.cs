using HIS.Application.DTOs.SystemUser;
using MediatR;

namespace HIS.Application.Queries.SystemUser;

public class GetSystemUserListQuery : IRequest<IEnumerable<SystemUserDto>>
{
    public bool IncludeInactive { get; set; } = false;
    public int? RoleId { get; set; }
}