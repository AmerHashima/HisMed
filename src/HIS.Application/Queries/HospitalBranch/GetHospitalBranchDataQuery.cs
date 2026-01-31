using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.HospitalBranch;
using MediatR;

namespace HIS.Application.Queries.HospitalBranch;

public record GetHospitalBranchDataQuery(QueryRequest QueryRequest) : IRequest<PagedResult<HospitalBranchDto>>;