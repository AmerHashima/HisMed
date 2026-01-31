using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Prescription;

public class CreatePrescriptionDto
{
    [Required(ErrorMessage = "Encounter is required")]
    public Guid EncounterId { get; set; }

    [Required(ErrorMessage = "Medication name is required")]
    [StringLength(255, ErrorMessage = "Medication name cannot exceed 255 characters")]
    public string MedicationName { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "Dosage cannot exceed 100 characters")]
    public string? Dosage { get; set; }

    [StringLength(100, ErrorMessage = "Frequency cannot exceed 100 characters")]
    public string? Frequency { get; set; }

    [StringLength(50, ErrorMessage = "Duration cannot exceed 50 characters")]
    public string? Duration { get; set; }

    public string? Instructions { get; set; }
}