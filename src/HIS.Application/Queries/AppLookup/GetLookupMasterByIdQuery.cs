using HIS.Application.DTOs.AppLookup;
using MediatR;

namespace HIS.Application.Queries.AppLookup;

public record GetLookupMasterByIdQuery(Guid Id) : IRequest<AppLookupMasterDto?>;