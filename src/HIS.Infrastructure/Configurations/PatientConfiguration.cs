using HIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.Infrastructure.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");

        builder.HasKey(p => p.Oid);
        builder.Property(p => p.Oid)
            .HasColumnName("PatientID")
            .HasDefaultValueSql("NEWID()");

        // MRN
        builder.Property(p => p.MRN)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasIndex(p => p.MRN)
            .IsUnique()
            .HasDatabaseName("IX_Patients_MRN");

        // Identifiers
        builder.Property(p => p.NationalID)
            .HasMaxLength(20);

        builder.HasIndex(p => p.NationalID)
            .IsUnique()
            .HasDatabaseName("IX_Patients_NationalID")
            .HasFilter("[NationalID] IS NOT NULL");

        builder.Property(p => p.PassportNumber)
            .HasMaxLength(20);

        builder.HasIndex(p => p.PassportNumber)
            .IsUnique()
            .HasDatabaseName("IX_Patients_PassportNumber")
            .HasFilter("[PassportNumber] IS NOT NULL");

        builder.Property(p => p.IdentifierType)
            .IsRequired()
            .HasMaxLength(20);

        // Arabic Names
        builder.Property(p => p.FirstNameAr)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.MiddleNameAr)
            .HasMaxLength(100);

        builder.Property(p => p.LastNameAr)
            .IsRequired()
            .HasMaxLength(100);

        // English Names
        builder.Property(p => p.FirstNameEn)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.MiddleNameEn)
            .HasMaxLength(100);

        builder.Property(p => p.LastNameEn)
            .IsRequired()
            .HasMaxLength(100);

        // Computed Full Names
        builder.Property(p => p.FullNameAr)
            .HasComputedColumnSql("([FirstNameAr] + ' ' + ISNULL([MiddleNameAr] + ' ', '') + [LastNameAr])", stored: false);

        builder.Property(p => p.FullNameEn)
            .HasComputedColumnSql("([FirstNameEn] + ' ' + ISNULL([MiddleNameEn] + ' ', '') + [LastNameEn])", stored: false);

        // Demographics
        builder.Property(p => p.Gender)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(1);

        builder.Property(p => p.BirthDate)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(p => p.MaritalStatus)
            .HasMaxLength(20);

        builder.Property(p => p.Nationality)
            .HasMaxLength(50);

        builder.Property(p => p.BloodGroup)
            .HasMaxLength(5);

        // Contact
        builder.Property(p => p.Mobile)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.Phone)
            .HasMaxLength(20);

        builder.Property(p => p.Email)
            .HasMaxLength(100);

        // Address
        builder.Property(p => p.AddressLine1)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.AddressLine2)
            .HasMaxLength(200);

        builder.Property(p => p.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.State)
            .HasMaxLength(100);

        builder.Property(p => p.PostalCode)
            .HasMaxLength(20);

        builder.Property(p => p.Country)
            .IsRequired()
            .HasMaxLength(100);

        // Emergency Contact
        builder.Property(p => p.EmergencyName)
            .HasMaxLength(150);

        builder.Property(p => p.EmergencyRelation)
            .HasMaxLength(50);

        builder.Property(p => p.EmergencyMobile)
            .HasMaxLength(20);

        // System Fields
        builder.Property(p => p.IsActive)
            .HasDefaultValue(true);

        // Audit fields from BaseEntity
        builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedDate")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.UpdatedAt)
            .HasColumnName("ModifiedDate");

        // Indexes for performance
        builder.HasIndex(p => new { p.LastNameEn, p.FirstNameEn })
            .HasDatabaseName("IX_Patients_NameEn");

        builder.HasIndex(p => new { p.LastNameAr, p.FirstNameAr })
            .HasDatabaseName("IX_Patients_NameAr");

        builder.HasIndex(p => p.Mobile)
            .HasDatabaseName("IX_Patients_Mobile");

        builder.HasIndex(p => p.Email)
            .HasDatabaseName("IX_Patients_Email");

        builder.HasIndex(p => p.Gender)
            .HasDatabaseName("IX_Patients_Gender");

        builder.HasIndex(p => p.BloodGroup)
            .HasDatabaseName("IX_Patients_BloodGroup");

        builder.HasIndex(p => p.Nationality)
            .HasDatabaseName("IX_Patients_Nationality");

        builder.HasIndex(p => p.IsActive)
            .HasDatabaseName("IX_Patients_IsActive");

        // Soft delete filter
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}