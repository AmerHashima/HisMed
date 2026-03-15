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

    public bool IsPrimary { get; set; } = false;
    //Navigational Properties
    public ICollection<emr_icd110> emr_Icd110 { get; set; }
}