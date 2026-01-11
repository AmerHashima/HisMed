using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HIS.Infrastructure.Repositories;

public class PatientRepository : BaseRepository<Patient>, IPatientRepository
{
    public PatientRepository(HISDbContext context) : base(context)
    {
    }

    public async Task<Patient?> GetByMRNAsync(string mrn, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .FirstOrDefaultAsync(p => p.MRN == mrn && !p.IsDeleted, cancellationToken);
    }

    public async Task<Patient?> GetByNationalIDAsync(string nationalId, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .FirstOrDefaultAsync(p => p.NationalID == nationalId && !p.IsDeleted, cancellationToken);
    }

    public async Task<Patient?> GetByPassportNumberAsync(string passportNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .FirstOrDefaultAsync(p => p.PassportNumber == passportNumber && !p.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<Patient>> GetActivePatientsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .Where(p => p.IsActive && !p.IsDeleted)
            .OrderBy(p => p.LastNameEn)
            .ThenBy(p => p.FirstNameEn)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        var search = searchTerm.ToLower();
        
        return await _context.Patients
            .Where(p => !p.IsDeleted && (
                p.FirstNameEn.ToLower().Contains(search) ||
                p.LastNameEn.ToLower().Contains(search) ||
                p.FirstNameAr.ToLower().Contains(search) ||
                p.LastNameAr.ToLower().Contains(search) ||
                p.MRN.ToLower().Contains(search) ||
                p.NationalID!.ToLower().Contains(search) ||
                p.PassportNumber!.ToLower().Contains(search) ||
                p.Mobile.ToLower().Contains(search)))
            .OrderBy(p => p.LastNameEn)
            .ThenBy(p => p.FirstNameEn)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> MRNExistsAsync(string mrn, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .AnyAsync(p => p.MRN == mrn && !p.IsDeleted, cancellationToken);
    }

    public async Task<bool> NationalIDExistsAsync(string nationalId, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .AnyAsync(p => p.NationalID == nationalId && !p.IsDeleted, cancellationToken);
    }

    public async Task<bool> PassportNumberExistsAsync(string passportNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .AnyAsync(p => p.PassportNumber == passportNumber && !p.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<Patient>> GetPatientsByGenderAsync(char gender, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .Where(p => p.Gender == gender && p.IsActive && !p.IsDeleted)
            .OrderBy(p => p.LastNameEn)
            .ThenBy(p => p.FirstNameEn)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Patient>> GetPatientsByBloodGroupAsync(string bloodGroup, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .Where(p => p.BloodGroup == bloodGroup && p.IsActive && !p.IsDeleted)
            .OrderBy(p => p.LastNameEn)
            .ThenBy(p => p.FirstNameEn)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Patient>> GetPatientsByNationalityAsync(string nationality, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .Where(p => p.Nationality == nationality && p.IsActive && !p.IsDeleted)
            .OrderBy(p => p.LastNameEn)
            .ThenBy(p => p.FirstNameEn)
            .ToListAsync(cancellationToken);
    }
}