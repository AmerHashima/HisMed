using HIS.Application.DTOs.Appointment;
using MediatR;

namespace HIS.Application.Queries.Appointment;

public record GetAppointmentByIdQuery(Guid Id) : IRequest<AppointmentDto?>;