using HIS.Domain.Common;
using HIS.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

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
        //modelBuilder.Entity<DoctorSchedule>(entity => { entity.Property(X => X.DayOfWeekId).HasColumnType("Guid"); });
        //modelBuilder.Entity<DoctorScheduleException>(entity => { entity.Property(X => X.DayOfWeekId).HasColumnType("Guid"); });
        //========================================================
        //                  Seed DaysData into tables
        //========================================================
        modelBuilder.Entity<AppLookupMaster>().HasData(
            
            new AppLookupMaster
            {
                Oid = Guid.Parse("10000000-0000-0000-0000-000000000010"),
                LookupCode = "Days",
                LookupNameAr = "الايام",
                LookupNameEn = "Days",
                Description = "Days Of The Week",
                CreatedAt = new DateTime(2026, 3, 3)

            }
        );
        modelBuilder.Entity<AppLookupDetail>().HasData(
                        new AppLookupDetail
                        {
                            Oid = Guid.Parse("10000000-0000-0000-0010-000000000001"),
                            MasterID = Guid.Parse("10000000-0000-0000-0000-000000000010"),
                            ValueCode = "Sat",
                            ValueNameAr = "السبت",
                            ValueNameEn = "Saturday",
                            SortOrder = 1,
                            IsDefault = false,
                            IsActive = true,
                            CreatedAt = new DateTime(2026, 3, 3)

                        },
            new AppLookupDetail
            {
                Oid = Guid.Parse("10000000-0000-0000-0010-000000000002"),
                MasterID = Guid.Parse("10000000-0000-0000-0000-000000000010"),
                ValueCode = "Sun",
                ValueNameAr = "الاحد",
                ValueNameEn = "Sunday",
                SortOrder = 2,
                IsDefault = false,
                IsActive = true,
                CreatedAt = new DateTime(2026, 3, 3)

            },
            new AppLookupDetail
            {
                Oid = Guid.Parse("10000000-0000-0000-0010-000000000003"),
                MasterID = Guid.Parse("10000000-0000-0000-0000-000000000010"),
                ValueCode = "Mon",
                ValueNameAr = "الاثنين",
                ValueNameEn = "Monday",
                SortOrder = 3,
                IsDefault = false,
                IsActive = true,
                CreatedAt = new DateTime(2026, 3, 3)

            },
            new AppLookupDetail
            {
                Oid = Guid.Parse("10000000-0000-0000-0010-000000000004"),
                MasterID = Guid.Parse("10000000-0000-0000-0000-000000000010"),
                ValueCode = "Tue",
                ValueNameAr = "الثلاثاء",
                ValueNameEn = "Tuesday",
                SortOrder = 4,
                IsDefault = false,
                IsActive = true,
                CreatedAt = new DateTime(2026, 3, 3)

            },
            new AppLookupDetail
            {
                Oid = Guid.Parse("10000000-0000-0000-0010-000000000005"),
                MasterID = Guid.Parse("10000000-0000-0000-0000-000000000010"),
                ValueCode = "Wed",
                ValueNameAr = "الاربعاء",
                ValueNameEn = "Wednesday",
                SortOrder = 5,
                IsDefault = false,
                IsActive = true,
                CreatedAt = new DateTime(2026, 3, 3)

            },
            new AppLookupDetail
            {
                Oid = Guid.Parse("10000000-0000-0000-0010-000000000006"),
                MasterID = Guid.Parse("10000000-0000-0000-0000-000000000010"),
                ValueCode = "Thu",
                ValueNameAr = "الخميس",
                ValueNameEn = "Thursday",
                SortOrder = 6,
                IsDefault = false,
                IsActive = true,
                CreatedAt = new DateTime(2026, 3, 3)

            }
            );



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