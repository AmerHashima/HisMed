using MediatR;

namespace HIS.Application.Commands.Doctor;

public record DeleteDoctorBranchCommand(Guid Id) : IRequest<bool>;
