using HIS.Application.DTOs.HospitalBranch;
using MediatR;

namespace HIS.Application.Queries.HospitalBranch;

public record GetHospitalBranchListQuery(bool ActiveOnly = true) : IRequest<IEnumerable<HospitalBranchDto>>;