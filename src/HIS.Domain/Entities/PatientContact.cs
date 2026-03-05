using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HIS.Domain.Common;

namespace HIS.Domain.Entities;

[Table("PatientContact")]
public class PatientContact : BaseEntity
{
    [Required]
    public Guid PatientId { get; set; }
    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    [MaxLength(200)]
    public string? ContactName { get; set; }

    public Guid? RelationshipId { get; set; }
    [ForeignKey(nameof(RelationshipId))]
    public virtual AppLookupDetail? Relationship { get; set; }

    [MaxLength(20)]
    public string? Mobile { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(200)]
    public string? Email { get; set; }
}
