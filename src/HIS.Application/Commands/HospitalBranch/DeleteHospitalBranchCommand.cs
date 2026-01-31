using MediatR;

namespace HIS.Application.Commands.HospitalBranch;

public record DeleteHospitalBranchCommand(Guid Id) : IRequest<bool>;