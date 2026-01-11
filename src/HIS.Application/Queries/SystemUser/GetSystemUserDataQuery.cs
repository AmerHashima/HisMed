using HIS.Application.DTOs.SystemUser;
using HIS.Application.DTOs.Common;
using MediatR;

namespace HIS.Application.Queries.SystemUser;

public record GetSystemUserDataQuery(QueryRequest QueryRequest) : IRequest<PagedResult<SystemUserDto>>;