using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.Doctor;
using MediatR;

namespace HIS.Application.Queries.Doctor;

public record GetDoctorDataQuery(QueryRequest QueryRequest) : IRequest<PagedResult<DoctorDto>>;