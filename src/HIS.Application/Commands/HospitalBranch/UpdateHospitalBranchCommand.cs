using HIS.Application.DTOs.HospitalBranch;
using MediatR;

namespace HIS.Application.Commands.HospitalBranch;

public record UpdateHospitalBranchCommand(UpdateHospitalBranchDto HospitalBranch) : IRequest<HospitalBranchDto>;