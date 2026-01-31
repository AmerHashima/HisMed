using HIS.Application.DTOs.Appointment;
using MediatR;

namespace HIS.Application.Commands.Appointment;

public record CreateAppointmentCommand(CreateAppointmentDto Appointment) : IRequest<AppointmentDto>;