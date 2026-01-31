using MediatR;

namespace HIS.Application.Commands.Appointment;

public record CancelAppointmentCommand(Guid Id, string? Reason = null) : IRequest<bool>;