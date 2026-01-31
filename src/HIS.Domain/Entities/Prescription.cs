using HIS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.Domain.Entities;

[Table("Prescriptions")]
public class Prescription : BaseEntity
{
    [Required]
    public Guid EncounterId { get; set; }
    [ForeignKey(nameof(EncounterId))]
    public virtual Encounter Encounter { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public string MedicationName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Dosage { get; set; }

    [MaxLength(100)]
    public string? Frequency { get; set; }

    [MaxLength(50)]
    public string? Duration { get; set; }

    public string? Instructions { get; set; }

}