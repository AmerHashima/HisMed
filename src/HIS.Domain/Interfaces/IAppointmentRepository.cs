using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface IAppointmentRepository : IBaseRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Appointment>> GetAppointmentsByDoctorAsync(Guid doctorId, DateTime date, CancellationToken cancellationToken = default);
    Task<IEnumerable<Appointment>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task<Appointment?> GetAppointmentWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
}