using HIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.Infrastructure.Configurations;

public class AppLookupMasterConfiguration : IEntityTypeConfiguration<AppLookupMaster>
{
    public void Configure(EntityTypeBuilder<AppLookupMaster> builder)
    {
        builder.ToTable("AppLookupMaster");

        builder.HasKey(x => x.Oid);
        builder.Property(x => x.Oid)
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.LookupCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.LookupCode)
            .IsUnique()
            .HasDatabaseName("IX_AppLookupMaster_LookupCode");

        builder.Property(x => x.LookupNameAr)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LookupNameEn)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(250);

        builder.Property(x => x.IsSystem)
            .HasDefaultValue(false);

        // Relationships
        builder.HasMany(x => x.LookupDetails)
            .WithOne(x => x.LookupMaster)
            .HasForeignKey(x => x.LookupMasterID)
            .HasConstraintName("FK_AppLookupDetail_Master")
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(x => x.IsSystem)
            .HasDatabaseName("IX_AppLookupMaster_IsSystem");

        builder.HasIndex(x => x.LookupNameEn)
            .HasDatabaseName("IX_AppLookupMaster_LookupNameEn");

        // Soft delete filter
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}