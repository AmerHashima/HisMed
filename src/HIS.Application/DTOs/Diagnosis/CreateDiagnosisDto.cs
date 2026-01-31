using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Diagnosis;

public class CreateDiagnosisDto
{
    [Required(ErrorMessage = "Encounter is required")]
    public Guid EncounterId { get; set; }

    [StringLength(20, ErrorMessage = "Diagnosis code cannot exceed 20 characters")]
    public string? DiagnosisCode { get; set; }

    [Required(ErrorMessage = "Diagnosis name is required")]
    [StringLength(255, ErrorMessage = "Diagnosis name cannot exceed 255 characters")]
    public string DiagnosisName { get; set; } = string.Empty;

    public bool IsPrimary { get; set; } = false;
}