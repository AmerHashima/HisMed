using HIS.Domain.Common;
using HIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Linq.Expressions;

namespace HIS.Infrastructure.Persistence;

public class HISDbContext : DbContext
{
    public HISDbContext(DbContextOptions<HISDbContext> options) : base(options)
    {
    }

    // Core System Tables
    public DbSet<SystemUser> SystemUsers { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    // Lookups
    public DbSet<AppLookupMaster> AppLookupMasters { get; set; }
    public DbSet<AppLookupDetail> AppLookupDetails { get; set; }
    
    // Hospital Structure
    public DbSet<HospitalBranch> HospitalBranches { get; set; }
    public DbSet<Specialty> Specialties { get; set; }
    
    // Medical Staff
    public DbSet<Doctor> Doctors { get; set; }
    
    // Patients
    public DbSet<Patient> Patients { get; set; }
    
    // Appointments & Encounters
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Encounter> Encounters { get; set; }
    public DbSet<Diagnosis> Diagnoses { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    
    // Doctor Scheduling
    public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
    public DbSet<DoctorScheduleException> DoctorScheduleExceptions { get; set; }
    public DbSet<DoctorTimeSlot> DoctorTimeSlots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasOne(p => p.IdentityType)
                  .WithMany()
                  .HasForeignKey(p => p.IdentityTypeLookupId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(p => p.Gender)
                  .WithMany()
                  .HasForeignKey(p => p.GenderLookupId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(p => p.Nationality)
                  .WithMany()
                  .HasForeignKey(p => p.NationalityLookupId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(p => p.MaritalStatus)
                  .WithMany()
                  .HasForeignKey(p => p.MaritalStatusLookupId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(p => p.BloodGroup)
                  .WithMany()
                  .HasForeignKey(p => p.BloodGroupLookupId)
                  .OnDelete(DeleteBehavior.NoAction);
        });
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasOne(a => a.Patient)
                  .WithMany(p => p.Appointments)
                  .HasForeignKey(a => a.PatientId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(a => a.Doctor)
                  .WithMany(d => d.Appointments)
                  .HasForeignKey(a => a.DoctorId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(a => a.Branch)
                  .WithMany()
                  .HasForeignKey(a => a.BranchId)
                  .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Encounter>(entity =>
        {
            entity.HasOne(e => e.Patient)
                  .WithMany(p => p.Encounters)
                  .HasForeignKey(e => e.PatientId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(e => e.Doctor)
                  .WithMany(d => d.Encounters)
                  .HasForeignKey(e => e.DoctorId)
                  .OnDelete(DeleteBehavior.NoAction);


            entity.HasOne(e => e.Branch)
                  .WithMany()
                  .HasForeignKey(e => e.BranchId)
                  .OnDelete(DeleteBehavior.NoAction);
        });

        // 🔹 Apply configurations
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    // You can set CreatedBy here if you have user context
                    // entry.Entity.CreatedBy = _currentUserService.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    // You can set UpdatedBy here if you have user context
                    // entry.Entity.UpdatedBy = _currentUserService.UserId;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}