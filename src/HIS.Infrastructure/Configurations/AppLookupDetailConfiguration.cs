using HIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.Infrastructure.Configurations;

public class AppLookupDetailConfiguration : IEntityTypeConfiguration<AppLookupDetail>
{
    public void Configure(EntityTypeBuilder<AppLookupDetail> builder)
    {
        builder.ToTable("AppLookupDetail");

        builder.HasKey(x => x.Oid);
        builder.Property(x => x.Oid)
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.LookupMasterID)
            .IsRequired();

        builder.Property(x => x.ValueCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.ValueNameAr)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ValueNameEn)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.SortOrder)
            .HasDefaultValue(1);

        builder.Property(x => x.IsDefault)
            .HasDefaultValue(false);

        // Composite unique constraint
        builder.HasIndex(x => new { x.LookupMasterID, x.ValueCode })
            .IsUnique()
            .HasDatabaseName("UQ_LookupDetail");

        // Additional indexes
        builder.HasIndex(x => x.LookupMasterID)
            .HasDatabaseName("IX_AppLookupDetail_MasterID");

        builder.HasIndex(x => x.SortOrder)
            .HasDatabaseName("IX_AppLookupDetail_SortOrder");

        builder.HasIndex(x => x.IsDefault)
            .HasDatabaseName("IX_AppLookupDetail_IsDefault");

        // Soft delete filter
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}