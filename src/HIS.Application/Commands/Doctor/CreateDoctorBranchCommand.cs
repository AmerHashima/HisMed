using HIS.Application.DTOs.Doctor;
using MediatR;

namespace HIS.Application.Commands.Doctor;

public record CreateDoctorBranchCommand(CreateDoctorBranchDto DoctorBranch) : IRequest<DoctorBranchDto>;
