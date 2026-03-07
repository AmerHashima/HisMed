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
    public DbSet<DoctorBranch> DoctorBranches { get; set; }
    public DbSet<DoctorAttachment> DoctorAttachments { get; set; }
    
    // Patients
    public DbSet<Patient> Patients { get; set; }
    public DbSet<PatientAddress> PatientAddresses { get; set; }
    public DbSet<PatientContact> PatientContacts { get; set; }
    public DbSet<PatientAttachment> PatientAttachments { get; set; }
    public DbSet<PatientInsurance> PatientInsurances { get; set; }
    
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

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasOne(d => d.User)
                  .WithMany()
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(d => d.Gender)
                  .WithMany()
                  .HasForeignKey(d => d.GenderId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(d => d.LicenseType)
                  .WithMany()
                  .HasForeignKey(d => d.LicenseTypeId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(d => d.Specialty)
                  .WithMany()
                  .HasForeignKey(d => d.SpecialtyId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(d => d.SubSpecialty)
                  .WithMany()
                  .HasForeignKey(d => d.SubSpecialtyId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(d => d.Department)
                  .WithMany()
                  .HasForeignKey(d => d.DepartmentId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(d => d.Branch)
                  .WithMany()
                  .HasForeignKey(d => d.BranchId)
                  .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<DoctorBranch>(entity =>
        {
            entity.HasOne(db => db.Doctor)
                  .WithMany(d => d.DoctorBranches)
                  .HasForeignKey(db => db.DoctorId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(db => db.Branch)
                  .WithMany()
                  .HasForeignKey(db => db.BranchId)
                  .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<DoctorAttachment>(entity =>
        {
            entity.HasOne(da => da.Doctor)
                  .WithMany(d => d.DoctorAttachments)
                  .HasForeignKey(da => da.DoctorId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(da => da.AttachmentType)
                  .WithMany()
                  .HasForeignKey(da => da.AttachmentTypeId)
                  .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<PatientAddress>(entity =>
        {
            entity.HasOne(a => a.Patient)
                  .WithMany(p => p.Addresses)
                  .HasForeignKey(a => a.PatientId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(a => a.Country)
                  .WithMany()
                  .HasForeignKey(a => a.CountryId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(a => a.City)
                  .WithMany()
                  .HasForeignKey(a => a.CityId)
                  .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<PatientContact>(entity =>
        {
            entity.HasOne(c => c.Patient)
                  .WithMany(p => p.Contacts)
                  .HasForeignKey(c => c.PatientId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(c => c.Relationship)
                  .WithMany()
                  .HasForeignKey(c => c.RelationshipId)
                  .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<PatientAttachment>(entity =>
        {
            entity.HasOne(a => a.Patient)
                  .WithMany(p => p.Attachments)
                  .HasForeignKey(a => a.PatientId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(a => a.AttachmentType)
                  .WithMany()
                  .HasForeignKey(a => a.AttachmentTypeId)
                  .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<PatientInsurance>(entity =>
        {
            entity.HasOne(i => i.Patient)
                  .WithMany(p => p.Insurances)
                  .HasForeignKey(i => i.PatientId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(i => i.InsuranceCompany)
                  .WithMany()
                  .HasForeignKey(i => i.InsuranceCompanyId)
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