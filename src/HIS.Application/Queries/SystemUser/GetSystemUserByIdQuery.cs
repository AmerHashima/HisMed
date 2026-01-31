using HIS.Application.DTOs.SystemUserSpace;
using MediatR;

namespace HIS.Application.Queries.SystemUserSpace;

public record GetSystemUserByIdQuery(Guid Id) : IRequest<SystemUserDto?>;