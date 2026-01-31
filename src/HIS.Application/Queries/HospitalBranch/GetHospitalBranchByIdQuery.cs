using HIS.Application.DTOs.HospitalBranch;
using MediatR;

namespace HIS.Application.Queries.HospitalBranch;

public record GetHospitalBranchByIdQuery(Guid Id) : IRequest<HospitalBranchDto?>;