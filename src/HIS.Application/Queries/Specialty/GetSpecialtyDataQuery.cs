using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.Specialty;
using MediatR;

namespace HIS.Application.Queries.Specialty;

public record GetSpecialtyDataQuery(QueryRequest QueryRequest) : IRequest<PagedResult<SpecialtyDto>>;