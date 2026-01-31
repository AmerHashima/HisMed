using HIS.Application.DTOs.Appointment;
using MediatR;

namespace HIS.Application.Commands.Appointment;

public record UpdateAppointmentCommand(UpdateAppointmentDto Appointment) : IRequest<AppointmentDto>;