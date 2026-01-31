using HIS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.Domain.Entities;

[Table("Doctors")]
public class Doctor : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual SystemUser User { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LicenseNumber { get; set; } = string.Empty;

    [Required]
    public Guid SpecialtyId { get; set; }
    [ForeignKey(nameof(SpecialtyId))]
    public virtual Specialty Specialty { get; set; } = null!;

    [Required]
    public Guid DepartmentLookupId { get; set; }
    [ForeignKey(nameof(DepartmentLookupId))]
    public virtual AppLookupDetail Department { get; set; } = null!;

    [Required]
    public Guid BranchId { get; set; }
    [ForeignKey(nameof(BranchId))]
    public virtual HospitalBranch Branch { get; set; } = null!;

    [MaxLength(50)]
    public string? NphiesProviderId { get; set; }

    public bool IsNphiesEnabled { get; set; } = true;
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public virtual ICollection<Appointment>? Appointments { get; set; }
    public virtual ICollection<Encounter>? Encounters { get; set; }
    public virtual ICollection<DoctorSchedule>? Schedules { get; set; }
    public virtual ICollection<DoctorScheduleException>? ScheduleExceptions { get; set; }
    public virtual ICollection<DoctorTimeSlot>? TimeSlots { get; set; }

}