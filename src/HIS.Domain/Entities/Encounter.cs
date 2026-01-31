using HIS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.Domain.Entities;

[Table("Encounters")]
public class Encounter : BaseEntity
{
    public Guid? AppointmentId { get; set; }
    [ForeignKey(nameof(AppointmentId))]
    public virtual Appointment? Appointment { get; set; }

    [Required]
    public Guid PatientId { get; set; }
    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    [Required]
    public Guid DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    public virtual Doctor Doctor { get; set; } = null!;

    [Required]
    public DateTime EncounterDate { get; set; }

    [MaxLength(50)]
    public string? EncounterType { get; set; }

    public string? Notes { get; set; }

    public Guid? BranchId { get; set; }
    [ForeignKey(nameof(BranchId))]
    public virtual HospitalBranch? Branch { get; set; }

    // Navigation Properties
    public virtual ICollection<Diagnosis>? Diagnoses { get; set; }
    public virtual ICollection<Prescription>? Prescriptions { get; set; }

}