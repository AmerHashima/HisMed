using HIS.Application.DTOs.SystemUser;
using MediatR;

namespace HIS.Application.Queries.SystemUser;

public class GetSystemUserByIdQuery : IRequest<SystemUserDto?>
{
    public Guid Id { get; set; }
}