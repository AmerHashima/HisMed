using HIS.Application.DTOs.AppLookup;
using MediatR;

namespace HIS.Application.Queries.AppLookup;

public record GetLookupMasterByCodeQuery(string LookupCode, bool IncludeDetails = true) : IRequest<AppLookupMasterDto?>;