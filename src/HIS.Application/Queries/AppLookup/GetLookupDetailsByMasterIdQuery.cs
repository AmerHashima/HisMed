using HIS.Application.DTOs.AppLookup;
using MediatR;

namespace HIS.Application.Queries.AppLookup;

public record GetLookupDetailsByMasterIdQuery(Guid MasterID) : IRequest<IEnumerable<AppLookupDetailDto>>;