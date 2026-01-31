using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HIS.Infrastructure.Repositories;

public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(HISDbContext context) : base(context)
    {
    }

    public async Task<Doctor?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .Include(d => d.User)
            .Include(d => d.Specialty)
            .Include(d => d.Department)
            .Include(d => d.Branch)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task<Doctor?> GetByLicenseNumberAsync(string licenseNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .Include(d => d.User)
            .Include(d => d.Specialty)
            .Include(d => d.Department)
            .Include(d => d.Branch)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(x => x.LicenseNumber == licenseNumber, cancellationToken);
    }

    public async Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyAsync(Guid specialtyId, CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .Include(d => d.User)
            .Include(d => d.Specialty)
            .Include(d => d.Department)
            .Include(d => d.Branch)
            .Where(x => !x.IsDeleted && x.SpecialtyId == specialtyId && x.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Doctor>> GetDoctorsByBranchAsync(Guid branchId, CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .Include(d => d.User)
            .Include(d => d.Specialty)
            .Include(d => d.Department)
            .Include(d => d.Branch)
            .Where(x => !x.IsDeleted && x.BranchId == branchId && x.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Doctor>> GetActiveDoctorsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Doctors
            .Include(d => d.User)
            .Include(d => d.Specialty)
            .Include(d => d.Department)
            .Include(d => d.Branch)
            .Where(x => !x.IsDeleted && x.IsActive)
            .OrderBy(d => d.User.FullName)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> LicenseNumberExistsAsync(string licenseNumber, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Doctors.Where(x => !x.IsDeleted && x.LicenseNumber == licenseNumber);
        
        if (excludeId.HasValue)
        {
            query = query.Where(x => x.Oid != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }
}