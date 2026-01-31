using HIS.Application.DTOs.SystemUserSpace;
using HIS.Application.DTOs.Common;
using MediatR;

namespace HIS.Application.Queries.SystemUserSpace;

public record GetSystemUserDataQuery(QueryRequest QueryRequest) : IRequest<PagedResult<SystemUserDto>>;