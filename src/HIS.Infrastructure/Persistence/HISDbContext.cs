using HIS.Domain.Common;
using HIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HIS.Infrastructure.Persistence;

public class HISDbContext : DbContext
{
    public HISDbContext(DbContextOptions<HISDbContext> options) : base(options)
    {
    }

    public DbSet<SystemUser> SystemUsers { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<AppLookupMaster> AppLookupMasters { get; set; } // Add this line
    public DbSet<AppLookupDetail> AppLookupDetails { get; set; } // Add this line

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}