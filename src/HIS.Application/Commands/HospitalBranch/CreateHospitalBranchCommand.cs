using HIS.Application.DTOs.HospitalBranch;
using MediatR;

namespace HIS.Application.Commands.HospitalBranch;

public record CreateHospitalBranchCommand(CreateHospitalBranchDto HospitalBranch) : IRequest<HospitalBranchDto>;