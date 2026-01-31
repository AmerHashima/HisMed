using HIS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.Domain.Entities;

[Table("Appointments")]
public class Appointment : BaseEntity
{
    [Required]
    public Guid PatientId { get; set; }
    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    [Required]
    public Guid DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    public virtual Doctor Doctor { get; set; } = null!;

    [Required]
    public DateTime AppointmentDate { get; set; }

    [MaxLength(50)]
    public string? AppointmentType { get; set; }

    [MaxLength(20)]
    public string? Status { get; set; } // Scheduled, Confirmed, Cancelled, Completed

    [MaxLength(255)]
    public string? Reason { get; set; }

    public Guid? BranchId { get; set; }
    [ForeignKey(nameof(BranchId))]
    public virtual HospitalBranch? Branch { get; set; }

    // Navigation Properties
    public virtual Encounter? Encounter { get; set; }
    public virtual DoctorTimeSlot? TimeSlot { get; set; }
}