using HIS.Domain.Common;
using HIS.Domain.Entities;

namespace HIS.Domain.Interfaces;

public interface IEncounterRepository : IBaseRepository<Encounter>
{
    Task<IEnumerable<Encounter>> GetEncountersByPatientAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Encounter>> GetEncountersByDoctorAsync(Guid doctorId, CancellationToken cancellationToken = default);
    Task<Encounter?> GetEncounterWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Encounter?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken = default);
}