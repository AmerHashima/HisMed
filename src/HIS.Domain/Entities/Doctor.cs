using HIS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.Domain.Entities;

[Table("Doctors")]
public class Doctor : BaseEntity
{
    public Guid? UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual SystemUser? User { get; set; }

    /* ==== Names (Arabic) ==== */
    [MaxLength(200)]
    public string? FirstNameAr { get; set; }

    [MaxLength(200)]
    public string? MiddleNameAr { get; set; }

    [MaxLength(200)]
    public string? LastNameAr { get; set; }

    /* ==== Names (English) ==== */
    [MaxLength(200)]
    public string? FirstNameEn { get; set; }

    [MaxLength(200)]
    public string? MiddleNameEn { get; set; }

    [MaxLength(200)]
    public string? LastNameEn { get; set; }

    /* ==== Demographics ==== */
    public Guid? GenderId { get; set; }
    [ForeignKey(nameof(GenderId))]
    public virtual AppLookupDetail? Gender { get; set; }

    /* ==== License ==== */
    [MaxLength(100)]
    public string? LicenseNumber { get; set; }

    public Guid? LicenseTypeId { get; set; }
    [ForeignKey(nameof(LicenseTypeId))]
    public virtual AppLookupDetail? LicenseType { get; set; }

    public DateOnly? LicenseIssueDate { get; set; }
    public DateOnly? LicenseExpiryDate { get; set; }

    /* ==== Specialty ==== */
    public Guid? SpecialtyId { get; set; }
    [ForeignKey(nameof(SpecialtyId))]
    public virtual Specialty? Specialty { get; set; }

    public Guid? SubSpecialtyId { get; set; }
    [ForeignKey(nameof(SubSpecialtyId))]
    public virtual AppLookupDetail? SubSpecialty { get; set; }

    /* ==== Department ==== */
    public Guid? DepartmentId { get; set; }
    [ForeignKey(nameof(DepartmentId))]
    public virtual AppLookupDetail? Department { get; set; }

    /* ==== Contact ==== */
    [MaxLength(20)]
    public string? Mobile { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(200)]
    public string? Email { get; set; }

    /* ==== Professional ==== */
    public int? YearsOfExperience { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? ConsultationFee { get; set; }

    /* ==== Branch ==== */
    public Guid? BranchId { get; set; }
    [ForeignKey(nameof(BranchId))]
    public virtual HospitalBranch? Branch { get; set; }

    /* ==== NPHIES ==== */
    [MaxLength(100)]
    public string? NphiesProviderId { get; set; }

    [MaxLength(100)]
    public string? NphiesLicenseNumber { get; set; }

    public bool IsNphiesEnabled { get; set; } = false;
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public virtual ICollection<Appointment>? Appointments { get; set; }
    public virtual ICollection<Encounter>? Encounters { get; set; }
    public virtual ICollection<DoctorScheduleMaster>? Schedules { get; set; }
    public virtual ICollection<DoctorScheduleException>? ScheduleExceptions { get; set; }
    public virtual ICollection<DoctorTimeSlot>? TimeSlots { get; set; }
    public virtual ICollection<DoctorBranch>? DoctorBranches { get; set; }
    public virtual ICollection<DoctorAttachment>? DoctorAttachments { get; set; }
}