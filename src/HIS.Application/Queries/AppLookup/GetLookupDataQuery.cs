using HIS.Application.DTOs.AppLookup;
using HIS.Application.DTOs.Common;
using MediatR;

namespace HIS.Application.Queries.AppLookup;

public record GetLookupDataQuery(QueryRequest QueryRequest) : IRequest<PagedResult<AppLookupMasterDto>>;