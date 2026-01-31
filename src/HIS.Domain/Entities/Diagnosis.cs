using HIS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.Domain.Entities;

[Table("Diagnoses")]
public class Diagnosis : BaseEntity
{
    [Required]
    public Guid EncounterId { get; set; }
    [ForeignKey(nameof(EncounterId))]
    public virtual Encounter Encounter { get; set; } = null!;

    [MaxLength(20)]
    public string? DiagnosisCode { get; set; } // ICD-10 code

    [Required]
    [MaxLength(255)]
    public string DiagnosisName { get; set; } = string.Empty;

    public bool IsPrimary { get; set; } = false;
}