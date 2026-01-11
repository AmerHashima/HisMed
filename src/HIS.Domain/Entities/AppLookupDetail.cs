using HIS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.Domain.Entities;

[Table("AppLookupDetail")]
public class AppLookupDetail : BaseEntity
{
    [Required]
    public Guid LookupMasterID { get; set; }

    [Required]
    [MaxLength(50)]
    public string ValueCode { get; set; } = string.Empty; // e.g. M, F, SINGLE

    [Required]
    [MaxLength(100)]
    public string ValueNameAr { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string ValueNameEn { get; set; } = string.Empty;

    public int SortOrder { get; set; } = 1;

    public bool IsDefault { get; set; } = false;

    // Navigation Properties
    [ForeignKey(nameof(LookupMasterID))]
    public virtual AppLookupMaster LookupMaster { get; set; } = null!;
}