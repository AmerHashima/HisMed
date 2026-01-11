using HIS.Application.DTOs.Patient;
using HIS.Application.DTOs.Common;
using MediatR;

namespace HIS.Application.Queries.Patient;

public record GetPatientDataQuery(QueryRequest QueryRequest) : IRequest<PagedResult<PatientDto>>;