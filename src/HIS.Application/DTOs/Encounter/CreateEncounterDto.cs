using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Encounter;

public class CreateEncounterDto
{
    public Guid? AppointmentId { get; set; }

    [Required(ErrorMessage = "Patient is required")]
    public Guid PatientId { get; set; }

    [Required(ErrorMessage = "Doctor is required")]
    public Guid DoctorId { get; set; }

    [Required(ErrorMessage = "Encounter date is required")]
    public DateTime EncounterDate { get; set; }

    [StringLength(50, ErrorMessage = "Encounter type cannot exceed 50 characters")]
    public string? EncounterType { get; set; }

    public string? Notes { get; set; }

    public Guid? BranchId { get; set; }
}