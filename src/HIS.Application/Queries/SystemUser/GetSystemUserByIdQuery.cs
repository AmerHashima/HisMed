using HIS.Application.DTOs.SystemUser;
using MediatR;

namespace HIS.Application.Queries.SystemUser;

public record GetSystemUserByIdQuery(Guid Id) : IRequest<SystemUserDto?>;