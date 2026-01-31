using MediatR;

namespace HIS.Application.Commands.Doctor;

public record DeleteDoctorCommand(Guid Id) : IRequest<bool>;