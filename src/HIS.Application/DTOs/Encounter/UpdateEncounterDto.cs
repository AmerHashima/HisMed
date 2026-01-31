using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Encounter;

public class UpdateEncounterDto
{
    [Required]
    public Guid Oid { get; set; }

    [Required(ErrorMessage = "Encounter date is required")]
    public DateTime EncounterDate { get; set; }

    [StringLength(50, ErrorMessage = "Encounter type cannot exceed 50 characters")]
    public string? EncounterType { get; set; }

    public string? Notes { get; set; }
}