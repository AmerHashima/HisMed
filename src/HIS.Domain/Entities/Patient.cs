using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using HIS.Domain.Common;

namespace HIS.Domain.Entities;

[Table("Patients")]
public class Patient : BaseEntity
{
    /* ==== Identifiers ==== */
    [Required]
    [MaxLength(50)]
    public string MRN { get; set; } = string.Empty; // Medical Record Number

    // Foreign Key for IdentityType
    [Required]
    public Guid IdentityTypeLookupId { get; set; }
    [ForeignKey(nameof(IdentityTypeLookupId))]
    public virtual AppLookupDetail IdentityType { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string IdentityNumber { get; set; } = string.Empty; // Unified identifier

    /* ==== Names (Arabic & English) ==== */
    [Required]
    [MaxLength(100)]
    public string FirstNameAr { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? MiddleNameAr { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastNameAr { get; set; } = string.Empty;

    // Computed property
    public string FullNameAr { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string FirstNameEn { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? MiddleNameEn { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastNameEn { get; set; } = string.Empty;

    // Computed property
    public string FullNameEn { get; set; } = string.Empty;

    /* ==== Demographics ==== */
    // Foreign Key for Gender
    [Required]
    public Guid GenderLookupId { get; set; }
    [ForeignKey(nameof(GenderLookupId))]
    public virtual AppLookupDetail Gender { get; set; } = null!;

    [Required]
    public DateOnly BirthDate { get; set; }

    // Foreign Key for Nationality (nullable)
    public Guid? NationalityLookupId { get; set; }
    [ForeignKey(nameof(NationalityLookupId))]
    public virtual AppLookupDetail? Nationality { get; set; }

    // Foreign Key for MaritalStatus (nullable)
    public Guid? MaritalStatusLookupId { get; set; }
    [ForeignKey(nameof(MaritalStatusLookupId))]
    public virtual AppLookupDetail? MaritalStatus { get; set; }

    // Foreign Key for BloodGroup (nullable)
    public Guid? BloodGroupLookupId { get; set; }
    [ForeignKey(nameof(BloodGroupLookupId))]
    public virtual AppLookupDetail? BloodGroup { get; set; }

    /* ==== Contact ==== */
    [Required]
    [MaxLength(20)]
    public string Mobile { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    /* ==== Branch ==== */
    [Required]
    public Guid BranchId { get; set; }
    [ForeignKey(nameof(BranchId))]
    public virtual HospitalBranch Branch { get; set; } = null!;

    /* ==== System Fields ==== */
    public bool IsActive { get; set; } = true;

    // Age calculation property
    public int Age => DateTime.Today.Year - BirthDate.Year - (DateTime.Today.DayOfYear < BirthDate.DayOfYear ? 1 : 0);

    // Navigation Properties
    public virtual ICollection<Appointment>? Appointments { get; set; }
    public virtual ICollection<Encounter>? Encounters { get; set; }


}