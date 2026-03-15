using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Diagnosis;

public class CreateDiagnosisDto
{
    [Required(ErrorMessage = "Encounter is required")]
    public Guid EncounterId { get; set; }

    public bool IsPrimary { get; set; } = false;
}