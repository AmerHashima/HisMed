using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HIS.Infrastructure.Repositories;

public class EncounterRepository : BaseRepository<Encounter>, IEncounterRepository
{
    public EncounterRepository(HISDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Encounter>> GetEncountersByPatientAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        return await _context.Encounters
            .Include(e => e.Patient)
            .Include(e => e.Doctor)
                .ThenInclude(d => d.User)
            .Include(e => e.Doctor)
                .ThenInclude(d => d.Specialty)
            .Include(e => e.Appointment)
            .Include(e => e.Branch)
            .Include(e => e.Diagnoses)
            .Include(e => e.Prescriptions)
            .Where(x => !x.IsDeleted && x.PatientId == patientId)
            .OrderByDescending(e => e.EncounterDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Encounter>> GetEncountersByDoctorAsync(Guid doctorId, CancellationToken cancellationToken = default)
    {
        return await _context.Encounters
            .Include(e => e.Patient)
                .ThenInclude(p => p.Gender)
            .Include(e => e.Patient)
                .ThenInclude(p => p.IdentityType)
            .Include(e => e.Doctor)
                .ThenInclude(d => d.User)
            .Include(e => e.Appointment)
            .Include(e => e.Branch)
            .Where(x => !x.IsDeleted && x.DoctorId == doctorId)
            .OrderByDescending(e => e.EncounterDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<Encounter?> GetEncounterWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Encounters
            .Include(e => e.Patient)
                .ThenInclude(p => p.Gender)
            .Include(e => e.Patient)
                .ThenInclude(p => p.IdentityType)
            .Include(e => e.Patient)
                .ThenInclude(p => p.Branch)
            .Include(e => e.Doctor)
                .ThenInclude(d => d.User)
            .Include(e => e.Doctor)
                .ThenInclude(d => d.Specialty)
            .Include(e => e.Doctor)
                .ThenInclude(d => d.Department)
            .Include(e => e.Appointment)
            .Include(e => e.Branch)
            .Include(e => e.Diagnoses)
            .Include(e => e.Prescriptions)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.Oid == id, cancellationToken);
    }

    public async Task<Encounter?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken = default)
    {
        return await _context.Encounters
            .Include(e => e.Patient)
            .Include(e => e.Doctor)
                .ThenInclude(d => d.User)
            .Include(e => e.Appointment)
            .Include(e => e.Diagnoses)
            .Include(e => e.Prescriptions)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.AppointmentId == appointmentId, cancellationToken);
    }
}