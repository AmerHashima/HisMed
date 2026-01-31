using HIS.Application.DTOs.Appointment;
using MediatR;

namespace HIS.Application.Queries.Appointment;

public record GetAppointmentListQuery(
    Guid? PatientId = null,
    Guid? DoctorId = null,
    DateTime? Date = null,
    DateTime? StartDate = null,
    DateTime? EndDate = null,
    string? Status = null
) : IRequest<IEnumerable<AppointmentDto>>;