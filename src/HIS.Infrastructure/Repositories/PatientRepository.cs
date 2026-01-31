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
            .Include(p => p.IdentityType)
            .Include(p => p.Gender)
            .Include(p => p.Nationality)
            .Include(p => p.MaritalStatus)
            .Include(p => p.BloodGroup)
            .Include(p => p.Branch)
            .FirstOrDefaultAsync(p => p.MRN == mrn && !p.IsDeleted, cancellationToken);
    }

    public async Task<Patient?> GetByIdentityNumberAsync(string identityNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .Include(p => p.IdentityType)
            .Include(p => p.Gender)
            .Include(p => p.Nationality)
            .Include(p => p.MaritalStatus)
            .Include(p => p.BloodGroup)
            .Include(p => p.Branch)
            .FirstOrDefaultAsync(p => p.IdentityNumber == identityNumber && !p.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<Patient>> GetActivePatientsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .Include(p => p.IdentityType)
            .Include(p => p.Gender)
            .Include(p => p.Nationality)
            .Include(p => p.MaritalStatus)
            .Include(p => p.BloodGroup)
            .Include(p => p.Branch)
            .Where(p => p.IsActive && !p.IsDeleted)
            .OrderBy(p => p.LastNameEn)
            .ThenBy(p => p.FirstNameEn)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        var search = searchTerm.ToLower();

        return await _context.Patients
            .Include(p => p.IdentityType)
            .Include(p => p.Gender)
            .Include(p => p.Nationality)
            .Include(p => p.MaritalStatus)
            .Include(p => p.BloodGroup)
            .Include(p => p.Branch)
            .Where(p => !p.IsDeleted && (
                p.FirstNameEn.ToLower().Contains(search) ||
                p.LastNameEn.ToLower().Contains(search) ||
                p.FirstNameAr.ToLower().Contains(search) ||
                p.LastNameAr.ToLower().Contains(search) ||
                p.MRN.ToLower().Contains(search) ||
                p.IdentityNumber.ToLower().Contains(search) ||
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

    public async Task<bool> IdentityNumberExistsAsync(string identityNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .AnyAsync(p => p.IdentityNumber == identityNumber && !p.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<Patient>> GetPatientsByGenderAsync(Guid genderLookupId, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .Include(p => p.IdentityType)
            .Include(p => p.Gender)
            .Include(p => p.Nationality)
            .Include(p => p.MaritalStatus)
            .Include(p => p.BloodGroup)
            .Include(p => p.Branch)
            .Where(p => p.GenderLookupId == genderLookupId && p.IsActive && !p.IsDeleted)
            .OrderBy(p => p.LastNameEn)
            .ThenBy(p => p.FirstNameEn)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Patient>> GetPatientsByBloodGroupAsync(Guid bloodGroupLookupId, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .Include(p => p.IdentityType)
            .Include(p => p.Gender)
            .Include(p => p.Nationality)
            .Include(p => p.MaritalStatus)
            .Include(p => p.BloodGroup)
            .Include(p => p.Branch)
            .Where(p => p.BloodGroupLookupId == bloodGroupLookupId && p.IsActive && !p.IsDeleted)
            .OrderBy(p => p.LastNameEn)
            .ThenBy(p => p.FirstNameEn)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Patient>> GetPatientsByNationalityAsync(Guid nationalityLookupId, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .Include(p => p.IdentityType)
            .Include(p => p.Gender)
            .Include(p => p.Nationality)
            .Include(p => p.MaritalStatus)
            .Include(p => p.BloodGroup)
            .Include(p => p.Branch)
            .Where(p => p.NationalityLookupId == nationalityLookupId && p.IsActive && !p.IsDeleted)
            .OrderBy(p => p.LastNameEn)
            .ThenBy(p => p.FirstNameEn)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Patient>> GetPatientsByBranchAsync(Guid branchId, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .Include(p => p.IdentityType)
            .Include(p => p.Gender)
            .Include(p => p.Nationality)
            .Include(p => p.MaritalStatus)
            .Include(p => p.BloodGroup)
            .Include(p => p.Branch)
            .Where(p => p.BranchId == branchId && p.IsActive && !p.IsDeleted)
            .OrderBy(p => p.LastNameEn)
            .ThenBy(p => p.FirstNameEn)
            .ToListAsync(cancellationToken);
    }
}